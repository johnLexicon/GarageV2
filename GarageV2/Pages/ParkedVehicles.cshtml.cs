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
    public class ParkedVehiclesModel : PageModel
    {

        [BindProperty]
        public IEnumerable<ParkedVehicle> ParkedVehicles { get; set; }

        public ParkedVehiclesModel(IVehiclesData vehiclesData)
        {
            ParkedVehicles = vehiclesData.GetAll();
        }

        public void OnGet()
        {
        }

        
    }
}
