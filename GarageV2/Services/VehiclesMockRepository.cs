using System;
using System.Collections.Generic;
using System.Linq;
using GarageV2.Models;

namespace GarageV2.Services
{
    public class VehiclesMockRepository : IVehiclesData
    {

        List<ParkedVehicle> _parkedVehicles;

        public VehiclesMockRepository()
        {
            _parkedVehicles = new List<ParkedVehicle>()
            {
                new ParkedVehicle()
                {
                    Id = 1,
                    RegNo = "ABC123",
                    Type = VehicleType.Airplane,
                    Brand = "Saab",
                    Model = "Jet349",
                    Color = "White",
                    NoWheels = 2
                },
                new ParkedVehicle()
                {
                    Id = 2,
                    RegNo = "CDE223",
                    Type = VehicleType.Car,
                    Brand = "Volvo",
                    Model = "740",
                    Color = "Green",
                    NoWheels = 4
                }
            };
        }

        public ParkedVehicle AddVehicle(ParkedVehicle vehicle)
        {
            vehicle.Id = _parkedVehicles.Max(v => v.Id) + 1;
            _parkedVehicles.Add(vehicle);

            return vehicle;
        }

        public IEnumerable<ParkedVehicle> GetAll()
        {
            return _parkedVehicles.OrderBy(v => v.Id);
        }
    }
}
