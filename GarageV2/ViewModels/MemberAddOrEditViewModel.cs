using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GarageV2.ViewModels
{
    public class MemberAddOrEditViewModel
    {
        //private string _email;

        public int Id { get; set; }

        [Remote(action: "CheckIfEmailAlreadyExists", controller: "Member", AdditionalFields = nameof(Id))]
        [Required(ErrorMessage = "E-post adress är obligatorisk")]
        [EmailAddress(ErrorMessage = "Fel format på e-post adress")]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [Display(Name = "Förnamn")]
        [StringLength(maximumLength: 20, ErrorMessage = "Förnamn får inte vara längre än 20 tecken")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [StringLength(maximumLength: 30, ErrorMessage = "Efternamn får inte vara längre än 20 tecken")]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Fel format för tel-nummer")]
        [MaxLength(20)]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

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
