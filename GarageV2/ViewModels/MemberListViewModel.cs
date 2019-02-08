using GarageV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.ViewModels
{
    public class MemberListViewModel
    {
        public int Id { get; set; }

        [EmailAddress]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }
        
        [Display(Name ="Fordon")]
        public int ParkedVehiclesCount { get; set; }
        //public ICollection<ParkedVehicle> ParkedVehicles { get; set; }
    }
}
