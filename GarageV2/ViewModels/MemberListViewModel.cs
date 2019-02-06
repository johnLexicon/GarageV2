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
        [Display(Name = "Telnr")]
        public string PhoneNumber { get; set; }
    }
}
