using System;
using GarageV2.Models;

namespace GarageV2.ViewModels
{
    public class ParkedVehiclesViewModel
    {
        public int Id { get; set;  }
        public string RegNo { get; set; }
        public VehicleType Type { get; set; }
        public string Color { get; set; }
        public TimeSpan TimeParked { get; set; }
    }
}
