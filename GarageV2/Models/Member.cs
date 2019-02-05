using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<ParkedVehicle> ParkedVehicles { get; set; }
    }
}
