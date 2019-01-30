using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GarageV2.Models
{
    public class GarageV2Context : DbContext
    {
        public GarageV2Context (DbContextOptions<GarageV2Context> options)
            : base(options)
        {
        }

        public DbSet<GarageV2.Models.ParkedVehicle> ParkedVehicle { get; set; }

    }
}
