﻿using GarageV2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Display(Name ="Ägare")]
        public Member Member { get; set; }

        [Display(Name = "Reg-nummer")]
        public string RegNo { get; set; }

        [Display(Name = "Fordonstyp")]
        public VehicleType VehicleType { get; set; }
        
        [Display(Name = "Märke")]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        public string Model { get; set; }

        [Display(Name = "Färg")]
        public string Color { get; set; }

        [Display(Name ="Parkeringstid")]
        public TimeSpan TimeParked { get; internal set; }

        public string FormatedTimeParked { get => TimeParked.ToString(@"d\.hh\:mm\:ss"); }
    }
}
