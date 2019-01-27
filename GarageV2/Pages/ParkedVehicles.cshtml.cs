using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GarageV2.Models;
using GarageV2.Services;
using GarageV2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GarageV2.Pages
{
    public class ParkedVehiclesModel : PageModel
    {

        [BindProperty]
        public IEnumerable<ParkedVehiclesViewModel> ParkedVehiclesViewModel { get; set; }

        public ParkedVehiclesModel(IVehiclesData vehiclesService, IMapper mapper)
        {
            _mapper = mapper;
            _vehiclesService = vehiclesService;
        }

        public void OnGet()
        {
            var parkedVehicles = _vehiclesService.GetAll();

            ParkedVehiclesViewModel = parkedVehicles.Select(pv => {
                var viewModel = _mapper.Map<ParkedVehiclesViewModel>(pv);
                viewModel.TimeParked = (DateTime.UtcNow.ToLocalTime() - pv.CheckIn);
                return viewModel;
            });
        }

        public IActionResult OnGetDelete(int id)
        {
            var removedVehicle = _vehiclesService.RemoveVehicle(id);
            return new RedirectToPageResult("Receipt", removedVehicle);
        }

        private readonly IMapper _mapper;
        private readonly IVehiclesData _vehiclesService;
    }
}
