using GarageV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.ViewModels
{
    public class DetailsMemberViewModel
    {

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        //public ICollection<ParkedVehicle> ParkedVehicles { get; set; }
        [Display(Name = "Parkerade fordon")]
        public ICollection<Tuple<int, string>> ParkedVehiclesInfo { get; set; }

    }
}
