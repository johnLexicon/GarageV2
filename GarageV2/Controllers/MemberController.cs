using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GarageV2.Models;
using GarageV2.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
                viewModel = new MemberAddOrEditViewModel();
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
    }
}