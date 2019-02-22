using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GarageV2.Models
{
    public class GarageV2Context : IdentityDbContext<Member>
    {
        public GarageV2Context (DbContextOptions<GarageV2Context> options)
            : base(options)
        {
        }

        /*** Properties of DbSets for entities that you want to interact with directly ***/

        public DbSet<ParkedVehicle> ParkedVehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Member> Members { get; set; }

    }
}
