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
    public class ReceiptModel : PageModel
    {

        public ReceiptViewModel ReceiptViewModel { get; set; }

        public ReceiptModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void OnGet(ParkedVehicle parkedVehicle)
        {
            ReceiptViewModel = _mapper.Map<ReceiptViewModel>(parkedVehicle);
        }

        private readonly IMapper _mapper;

    }
}
