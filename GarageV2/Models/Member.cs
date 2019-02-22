using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GarageV2.Models
{
    public class Member : IdentityUser
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ParkedVehicle> ParkedVehicles { get; set; }

    }
}
