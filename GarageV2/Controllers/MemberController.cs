using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GarageV2.Models;
using GarageV2.Services;
using GarageV2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarageV2.Controllers
{
    public class MemberController : Controller
    {

        private readonly GarageV2Context _context;
        private readonly IMapper _mapper;
        private readonly ParkedVehicleGenerator _parkedVehicleGenerator;

        public MemberController(GarageV2Context context, IMapper mapper, ParkedVehicleGenerator parkedVehicleGenerator)
        {
            _context = context;
            _mapper = mapper;
            _parkedVehicleGenerator = parkedVehicleGenerator;
        }
        
        public IActionResult Index()
        {
            return View();
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

        [Route("Member/Generate/{noMembers}")]
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