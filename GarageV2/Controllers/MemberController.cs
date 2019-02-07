using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GarageV2.Models;
using GarageV2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarageV2.Controllers
{
    public class MemberController : Controller
    {

        private readonly GarageV2Context _context;
        private readonly IMapper _mapper;

        public MemberController(GarageV2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public IActionResult Index()
        {
            var members = _context.Member.ToList();
            IEnumerable<MemberListViewModel> memberListViewModels = members.Select(m =>
            {
                return _mapper.Map<MemberListViewModel>(m);
            });
            return View(memberListViewModels);
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            MemberAddOrEditViewModel viewModel = null;

            //Create
            if(id == 0)
            {
                viewModel = new MemberAddOrEditViewModel();
            }
            //Edit
            else
            {
                var member = _context.Member.Find(id);
                viewModel = _mapper.Map<MemberAddOrEditViewModel>(member);
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddOrEdit(MemberAddOrEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var member = _mapper.Map<Member>(viewModel);

                //Create
                if (viewModel.Id == 0)
                {
                    _context.Add(member);
                }
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

            var foundMember = _context.Member.FirstOrDefault(p => p.Email.ToLower().Equals(email.ToLower()));
            if (foundMember != null && foundMember.Id != id)
            {
                return Json($"E-post adressen {email} är redan registrerad");
            }
            return Json(true);
        }

    }
}