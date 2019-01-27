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

        public ParkedVehiclesModel(IVehiclesData vehiclesData, IMapper mapper)
        {
            var parkedVehicles = vehiclesData.GetAll();
            ParkedVehiclesViewModel = parkedVehicles.Select(pv => {
                    var viewModel = mapper.Map<ParkedVehiclesViewModel>(pv);
                    viewModel.TimeParked = (DateTime.UtcNow.ToLocalTime() - pv.CheckIn);
                    return viewModel;
                }
            );
        }

        public void OnGet()
        {
        }

        
    }
}
