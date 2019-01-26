using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarageV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GarageV2.Pages
{
    public class ParkVehicleModel : PageModel
    {

        [BindProperty]
        public ParkedVehicle ParkedVehicle { get; set;  }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                return new RedirectToPageResult("VehicleDetails");
            }

            return Page();
        }
    }
}
