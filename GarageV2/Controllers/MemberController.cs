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
        private readonly UserManager<Member> _userManager;

        //TODO: Check if you at the end need the actual context of if it is enough with the userManager.
        public MemberController(GarageV2Context context, IMapper mapper, ParkedVehicleGenerator parkedVehicleGenerator, UserManager<Member> userManager)
        {
            _context = context;
            _mapper = mapper;
            _parkedVehicleGenerator = parkedVehicleGenerator;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index(string searchString)
        {
            // Query for retrieving all members
            var members = from m in _context.Members                        
                        select m;

            // - Filter -
            if (!String.IsNullOrEmpty(searchString))
            {
                // Query for retrieving members that contain the searchstring
                members = _context.Members                
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


        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var member = _userManager.Users
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

        public async Task<IActionResult> Edit(string id)
        {
            var member = await _userManager.FindByIdAsync(id);
            if(member is null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<EditMemberViewModel>(member);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMemberViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var member = await _userManager.FindByIdAsync(viewModel.Id);

                //Map to the existing object instead of creating a new instance of it.
                //The properties in the destination that not exist in the source are left with their existing values.
                _mapper.Map(viewModel, member);

                var updateResult = await _userManager.UpdateAsync(member);

                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError("error", error.Description);
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var member = _mapper.Map<Member>(viewModel);

                var registerResult = await _userManager.CreateAsync(member, viewModel.Password);

                if (!registerResult.Succeeded)
                {
                    foreach(var error in registerResult.Errors)
                    {
                        ModelState.AddModelError("error", error.Description);
                    }

                    return View(viewModel);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        /// <summary>
        /// Helper Action used for Remote validation for the Email uniqueness in the view class ViewModels/MemberAddOrEditViewModel.cs
        /// </summary>
        /// <param name="email">The email to check</param>
        /// <returns></returns>
        public async Task<IActionResult> CheckIfEmailAlreadyExists(string email)
        {

            var foundMember = await _userManager.FindByEmailAsync(email);
            if (foundMember != null)
            {
                return Json($"E-post adressen {email} är redan registrerad");
            }
            return Json(true);
        }

        [Route("Member/Generate/{noMembers?}")]
        public async Task<IActionResult> GenerateMembers(int noMembers = 5)
        {
            //Retrieve the existing emails to be able to verify the generated ones do not already exists.
            var existingEmails = await _context.Members.Select(m => m.Email).ToListAsync();

            int generatedMembers = 0;
            while(generatedMembers < noMembers)
            {
                var generatedEmail = _parkedVehicleGenerator.GenerateEmail();

                //If email does not exists
                if (existingEmails.IndexOf(generatedEmail) == -1)
                {
                    var member = _parkedVehicleGenerator.GenerateMember(generatedEmail);
                    var identityResult = await _userManager.CreateAsync(member, "secret123");

                    if (identityResult.Succeeded)
                    {
                        existingEmails.Add(generatedEmail);
                        generatedMembers++;
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

    }
}