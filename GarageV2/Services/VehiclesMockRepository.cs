using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    NoWheels = 2,
                    CheckIn = DateTime.UtcNow.ToLocalTime()
                },
                new ParkedVehicle()
                {
                    Id = 2,
                    RegNo = "CDE223",
                    Type = VehicleType.Car,
                    Brand = "Volvo",
                    Model = "740",
                    Color = "Green",
                    NoWheels = 4,
                    CheckIn = DateTime.UtcNow.ToLocalTime()
                }
            };
        }

        public VehiclesMockRepository(int noVehicles)
        {
            _parkedVehicles = GenerateVehicles(noVehicles);
            
        }

        public ParkedVehicle AddVehicle(ParkedVehicle vehicle)
        {
            vehicle.Id = _parkedVehicles.Max(v => v.Id) + 1;
            vehicle.CheckIn = DateTime.UtcNow.ToLocalTime();
            _parkedVehicles.Add(vehicle);

            return vehicle;
        }

        public IEnumerable<ParkedVehicle> GetAll()
        {
            return _parkedVehicles.OrderBy(v => v.Id);
        }

        private List<ParkedVehicle> GenerateVehicles(int noVehicles)
        {
            Random rnd = new Random();
            var parkedVehicles = new List<ParkedVehicle>();

            for(int i = 0; i < noVehicles; i++)
            {
                parkedVehicles.Add(new ParkedVehicle
                {
                    Id = (i + 1),
                    RegNo = GenerateRegNo(),
                    Type = (VehicleType)rnd.Next(5),
                    Brand = "WhateverBrand",
                    Model = "WhateverModel",
                    Color = "WhateverColor",
                    NoWheels = rnd.Next(10),
                    CheckIn = DateTime.UtcNow.ToLocalTime()
                });
            }

            return parkedVehicles;
        }
            
        private string GenerateRegNo()
        {
            Random rnd = new Random();
            StringBuilder stb = new StringBuilder();

            stb.Append((char)rnd.Next(65, 90));
            stb.Append((char)rnd.Next(65, 90));
            stb.Append((char)rnd.Next(65, 90));
            stb.Append(rnd.Next(10));
            stb.Append(rnd.Next(10));
            stb.Append(rnd.Next(10));

            return stb.ToString();
        }
    }
}
