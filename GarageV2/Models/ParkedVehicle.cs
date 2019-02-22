using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.Models
{

    /// <summary>
    /// Model used for the parked vehicles.
    /// </summary>
    public class ParkedVehicle
    {

        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{3}\d{3}$", ErrorMessage = "Fel format för reg-nummer")]
        [Display(Name = "Reg-nummer")]
        public string RegNo { get; set; }

        [Display(Name = "Färg")]
        [StringLength(8, MinimumLength = 3, ErrorMessage = "Färg ska vara en sträng mellan 3 till 8 tecken")]
        public string Color { get; set; }

        [Display(Name = "Märke")]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        public string Model { get; set; }

        [Range(0, 18, ErrorMessage = "Kan endast vara 0 till 18 däck")]
        [Display(Name = "Antal däck")]
        public int NoWheels { get; set; }

        [Display(Name = "Incheckning")]
        public DateTime CheckIn { get; set; }

        public VehicleType VehicleType { get; set; }

        [Required]
        public Member Member { get; set; }
    }
}
