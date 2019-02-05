using System;
using System.Text;
using GarageV2.Models;

namespace GarageV2.Services
{
    public class ParkedVehicleGenerator
    {
        Random _rnd;

        public ParkedVehicleGenerator()
        {
            _rnd = new Random();
        }

        /// <summary>
        /// Generates a parked vehicle randomizing the Vehicle type, Reg number and number or wheels.
        /// </summary>
        /// <returns></returns>
        public ParkedVehicle GenerateVehicle()
        {
            DateTime dateTime = DateTime.UtcNow.ToLocalTime();
            dateTime = dateTime.AddMinutes(-(_rnd.Next(300)));

            return new ParkedVehicle
            {
                RegNo = GenerateRegNo(),
                //ParkedVehicleType = (VehicleType)_rnd.Next(5),
                Brand = "TheBrand",
                Model = "TheModel",
                Color = "TheColor",
                NoWheels = _rnd.Next(10),
                CheckIn = dateTime
            };
        }

        /// <summary>
        /// Method for generating the reg number
        /// </summary>
        /// <returns></returns>
        private string GenerateRegNo()
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
