using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GarageV2.Models;
using GarageV2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GarageV2.Pages
{
    public class VehicleDetailsModel : PageModel
    {

        [BindProperty]
        public VehicleDetailsViewModel VehicleDetails { get; set; }

        public VehicleDetailsModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void OnGet(ParkedVehicle parkedVehicle)
        {
            VehicleDetails = _mapper.Map<VehicleDetailsViewModel>(parkedVehicle);
        }

        private readonly IMapper _mapper;

    }
}
