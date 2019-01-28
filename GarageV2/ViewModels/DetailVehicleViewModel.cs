using GarageV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.ViewModels
{
    public class DetailVehicleViewModel
    {
        [Display(Name = "Reg-nummer")]
        public string RegNo { get; set; }
        [Display(Name = "Fordonstyp")]
        public VehicleType ParkedVehicleType { get; set; }
        [Display(Name = "Färg")]
        public string Color { get; set; }
        [Display(Name = "Märke")]
        public string Brand { get; set; }
        [Display(Name = "Modell")]
        public string Model { get; set; }
        [Display(Name = "Antal däck")]
        public int NoWheels { get; set; }

    }
}
