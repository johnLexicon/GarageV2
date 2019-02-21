using System;
using System.ComponentModel.DataAnnotations;

namespace GarageV2.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "E-post adress är obligatorisk")]
        [EmailAddress(ErrorMessage = "Fel format på e-post adress")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

    }
}
