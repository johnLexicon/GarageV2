using GarageV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.ViewModels
{
    public class ReceiptParkingViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Reg-nummer")]
        public string RegNo { get; set; }
        [Display(Name = "Fordonstyp")]
        public VehicleType ParkedVehicleType { get; set; }
        [Display(Name = "Färg")]
        public string Color { get; set; }
        public TimeSpan TimeParked { get; set; }
    }
}
