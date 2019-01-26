using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarageV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GarageV2.Pages
{
    public class VehicleDetailsModel : PageModel
    {

        [BindProperty]
        public ParkedVehicle ParkedVehicle { get; set; }

        public void OnGet(ParkedVehicle parkedVehicle)
        {
            ParkedVehicle = parkedVehicle;
        }
    }
}
