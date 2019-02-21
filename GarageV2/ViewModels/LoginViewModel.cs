using System;
using System.ComponentModel.DataAnnotations;

namespace GarageV2.ViewModels
{
    public class LoginViewModel
    {

        [Display(Name = "Användarnamn")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Bekräfta lösenord")]
        [DataType(DataType.Password)]
        [Required]
        [CompareAttribute("Password", ErrorMessage = "Lösenord ej lika")]
        public string PasswordVerify { get; set; }
    }
}
