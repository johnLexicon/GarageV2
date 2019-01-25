using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.Models
{

    public enum VehicleType
    {
        [Display(Name = "Bil")]
        Car,
        [Display(Name = "Båt")]
        Boat,
        [Display(Name = "Motorcykel")]
        Motorbike,
        [Display(Name = "Flygplan")]
        Airplane,
        [Display(Name = "Cykel")]
        Bicycle
    }
    //public enum vehicleModel
    //{
    //    Sedan,
    //    Hardtop,
    //    Cabrolie,
    //    Other
    //}

    public class ParkedVehicle
    {

        public int Id { get; set; }
        [Display(Name = "Registreringsnummber")]
        public string RegNo { get; set; }
        [Display(Name = "Fordonstyp")]
        public VehicleType Type { get; set; }
        [Display(Name = "Färg")]
        public string Color { get; set; }
        [Display(Name = "Märke")]
        public string Brand { get; set; }
        [Display(Name = "Modell")]
        public string Model { get; set; }
        [Display(Name = "Antal däck")]
        public int NoWheels { get; set; }
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }
}
