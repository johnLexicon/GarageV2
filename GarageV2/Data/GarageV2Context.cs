using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GarageV2.Models
{
    public class GarageV2Context : IdentityDbContext<IdentityUser>
    {
        public GarageV2Context (DbContextOptions<GarageV2Context> options)
            : base(options)
        {
        }

        public DbSet<GarageV2.Models.ParkedVehicle> ParkedVehicle { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<Member> Member { get; set; }

    }
}
