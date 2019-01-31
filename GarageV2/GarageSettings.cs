using System;
using Microsoft.Extensions.Configuration;

namespace GarageV2
{
    /// <summary>
    /// Helper class for retrieving Garage settings from the appsettings.json file
    /// </summary>
    public class GarageSettings
    {
        public GarageSettings(IConfiguration configuration)
        {
            PricePerMinute = decimal.Parse(configuration["GaragePricePerMinute"]);
        }

        public decimal PricePerMinute { get; }
    }
}
