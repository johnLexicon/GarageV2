using GarageV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.ViewModels
{
    public class AddOrEditViewModel
    {
        private string _regNo;

        public int Id { get; set; }

        public string RegNo { get { return _regNo; } set { _regNo = value.ToUpper(); } }

        public VehicleType ParkedVehicleType { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int NoWheels { get; set; }

        public DateTime CheckIn { get; set; }

        public bool AlreadyParked { get; set; }
    }
}
