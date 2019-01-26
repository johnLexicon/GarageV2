using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarageV2.Models;
using GarageV2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GarageV2.Pages
{
    public class ParkVehicleModel : PageModel
    {

        [BindProperty]
        public ParkedVehicle ParkedVehicle { get; set;  }


        public ParkVehicleModel(IVehiclesData vehiclesData)
        {
            _vehiclesData = vehiclesData;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                var addedVehicle = _vehiclesData.AddVehicle(ParkedVehicle);

                //TODO: Add functionality for case addedVehicle == null

                return new RedirectToPageResult("VehicleDetails", addedVehicle);
            }

            return Page();
        }

        private readonly IVehiclesData _vehiclesData;
    }
}
