using System;
using Microsoft.Extensions.Configuration;

namespace GarageV2
{
    public class GarageSettings
    {
        public GarageSettings(IConfiguration configuration)
        {
            PricePerMinute = decimal.Parse(configuration["GaragePricePerMinute"]);
        }

        public decimal PricePerMinute { get; }
    }
}
