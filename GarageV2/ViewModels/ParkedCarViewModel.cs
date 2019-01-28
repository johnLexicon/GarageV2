using GarageV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.ViewModels
{
    public class ParkedCarViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Reg-nummer")]
        public string RegNo { get; set; }
        [Display(Name = "Fordonstyp")]
        public VehicleType Type { get; set; }
        [Display(Name = "Färg")]
        public string Color { get; set; }
        [Display(Name ="Parkeringstid")]
        public TimeSpan TimeParked { get; internal set; }
    }
}
