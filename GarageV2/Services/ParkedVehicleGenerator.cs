using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarageV2.Models;
using Microsoft.AspNetCore.Identity;

namespace GarageV2.Services
{
    public class ParkedVehicleGenerator
    {
        private readonly Random _rnd;
        private readonly GarageV2Context _context;

        public ParkedVehicleGenerator(GarageV2Context context)
        {
            _rnd = new Random();
            _context = context;
        }

        /// <summary>
        /// Generates a parked vehicle randomizing the Vehicle type, Reg number and number or wheels.
        /// </summary>
        /// <returns></returns>
        public ParkedVehicle GenerateVehicle(string regNo)
        {
            DateTime dateTime = DateTime.UtcNow.ToLocalTime();
            dateTime = dateTime.AddMinutes(-(_rnd.Next(300)));
            var vehicleTypes = _context.VehicleTypes.ToList();
            VehicleType vehicleType = vehicleTypes.ElementAt(_rnd.Next(vehicleTypes.Count - 1));

            return new ParkedVehicle
            {
                RegNo = regNo,
                VehicleType = vehicleType,
                Brand = "TheBrand",
                Model = "TheModel",
                Color = "TheColor",
                NoWheels = _rnd.Next(10),
                CheckIn = dateTime
            };
        }

        public Member GenerateMember(string email)
        {
            return new Member
            {
                Email = email,
                UserName = email,
                FirstName = "First name",
                LastName = "Last name",
                PhoneNumber = _rnd.Next(1000000000).ToString()
            };
        }

        public string GenerateEmail()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append((char)_rnd.Next(97, 122));
            stb.Append((char)_rnd.Next(97, 122));
            stb.Append((char)_rnd.Next(97, 122));
            stb.Append((char)_rnd.Next(97, 122));
            stb.Append((char)_rnd.Next(97, 122));
            stb.Append((char)_rnd.Next(97, 122));
            stb.Append("@gmail.com");

            return stb.ToString();
        }

        /// <summary>
        /// Method for generating the reg number
        /// </summary>
        /// <returns></returns>
        public string GenerateRegNo()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append((char)_rnd.Next(65, 90));
            stb.Append((char)_rnd.Next(65, 90));
            stb.Append((char)_rnd.Next(65, 90));
            stb.Append(_rnd.Next(10));
            stb.Append(_rnd.Next(10));
            stb.Append(_rnd.Next(10));

            return stb.ToString();
        }
    }
}
