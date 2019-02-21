using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GarageV2.Models;
using GarageV2.Services;
using GarageV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GarageV2.Controllers
{
    public class MemberController : Controller
    {

        private readonly GarageV2Context _context;
        private readonly IMapper _mapper;
        private readonly ParkedVehicleGenerator _parkedVehicleGenerator;
        private readonly UserManager<IdentityUser> _userManager;

        public MemberController(GarageV2Context context, IMapper mapper, ParkedVehicleGenerator parkedVehicleGenerator, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _parkedVehicleGenerator = parkedVehicleGenerator;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index(string searchString)
        {
            // Query for retrieving all members
            var members = from m in _context.Member                        
                        select m;

            // - Filter -
            if (!String.IsNullOrEmpty(searchString))
            {
                // Query for retrieving members that contain the searchstring
                members = _context.Member                
                .Where(
                    pv => pv.Email.ToLower().Contains(searchString.ToLower()) ||                    
                    pv.FirstName.ToLower().Contains(searchString.ToLower()) ||
                    pv.LastName.ToLower().Contains(searchString.ToLower()) ||
                    pv.PhoneNumber.ToLower().Contains(searchString.ToLower())                    
                );
            }

            // --- Create MemberListViewModel ---

            var memberListViewModels = await members
                //.Include(m => m.ParkedVehicles)
                .ProjectTo<MemberListViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return View(memberListViewModels);
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = _context.Member
                .Include(m => m.ParkedVehicles)
                .FirstOrDefault(m => m.Id == id);

            if (member is null)
            {
                return NotFound();
            }

            //_context.Entry(member).Collection(m => m.ParkedVehicles).Load();
            DetailsMemberViewModel viewModel = _mapper.Map<DetailsMemberViewModel>(member);

            return View(viewModel);

        }
        
        public IActionResult AddOrEdit(int id = 0)
        {
            MemberAddOrEditViewModel viewModel = null;

            //Create
            if(id == 0)
            {
                return View(new MemberAddOrEditViewModel());
            }
            
            //Edit
            var member = _context.Member.Find(id);
            viewModel = _mapper.Map<MemberAddOrEditViewModel>(member);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(MemberAddOrEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var member = _mapper.Map<Member>(viewModel);

                //Create
                //If create then also register with Core identity.
                if (viewModel.Id == 0)
                {
                    var registerResult = await _userManager.CreateAsync(
                        new IdentityUser
                        {
                            UserName = viewModel.Email,
                            Email = viewModel.Email
                        }, viewModel.Password);

                    if (!registerResult.Succeeded)
                    {
                        foreach(var error in registerResult.Errors)
                        {
                            ModelState.AddModelError("error", error.Description);
                        }

                        return View(viewModel);
                    }

                    _context.Add(member);
                }
                //Edit
                //TODO: Important remove possibility to change email 
                //TODO: Fix password change.
                else
                {
                    _context.Update(member);
                }

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        /// <summary>
        /// Helper Action used for Remote validation for the Email uniqueness in the view class ViewModels/MemberAddOrEditViewModel.cs
        /// </summary>
        /// <param name="email">The email to check</param>
        /// <param name="id">The member id</param>
        /// <returns></returns>
        public IActionResult CheckIfEmailAlreadyExists(string email, int id)
        {
            if(email is null) 
            {
                return NotFound();
            }
            //TODO: Remove toLower check if it works using property with field in the view.
            var foundMember = _context.Member.FirstOrDefault(p => p.Email.ToLower().Equals(email.ToLower()));
            if (foundMember != null && foundMember.Id != id)
            {
                return Json($"E-post adressen {email} är redan registrerad");
            }
            return Json(true);
        }

        [Route("Member/Generate/{noMembers?}")]
        public IActionResult GenerateMembers(int noMembers = 5)
        {
            var existingEmails = _context.Member.Select(m => m.Email).ToList();
            for(int i = 0; i < noMembers; i++)
            {
                var member = _parkedVehicleGenerator.GenerateMember();
                //If email does noe exist
                if(existingEmails.IndexOf(member.Email) == -1)
                {
                    existingEmails.Add(member.Email);
                    _context.Add(member);
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}