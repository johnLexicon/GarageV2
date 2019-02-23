using System;
using System.ComponentModel.DataAnnotations;

namespace GarageV2.ViewModels
{
    public class EditMemberViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Förnamn")]
        [StringLength(maximumLength: 20, ErrorMessage = "Förnamn får inte vara längre än 20 tecken")]
        [Required(ErrorMessage = "Förnamn är obligatorisk")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [StringLength(maximumLength: 30, ErrorMessage = "Efternamn får inte vara längre än 20 tecken")]
        [Required(ErrorMessage = "Efternamn är obligatorisk")]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Fel format för tel-nummer")]
        [MaxLength(20)]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

    }
}
