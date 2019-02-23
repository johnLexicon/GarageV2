using GarageV2.Models;
using System;
using System.ComponentModel.DataAnnotations;


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

        public DateTime CheckIn { get; set; }

        [Display(Name ="Parkeringstid")]
        public TimeSpan TimeParked { get => DateTime.UtcNow.ToLocalTime() - CheckIn; }

        public string FormatedTimeParked { get => TimeParked.ToString(@"d\.hh\:mm\:ss"); }
    }
}
