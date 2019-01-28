using System;
using GarageV2.Models;

namespace GarageV2.ViewModels
{
    public class ReceiptViewModel
    {

        public string RegNo { get; set; }
        public VehicleType Type { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NoWheels { get; set; }
        public decimal Price { get; set; }

    }
}
