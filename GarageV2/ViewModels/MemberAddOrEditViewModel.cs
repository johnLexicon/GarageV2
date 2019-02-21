using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GarageV2.ViewModels
{
    public class MemberAddOrEditViewModel
    {

        public int Id { get; set; }

        public bool IsEditMode { get => Id != 0; }

        [Remote(action: "CheckIfEmailAlreadyExists", controller: "Member", AdditionalFields = nameof(Id))]
        [Required(ErrorMessage = "E-post adress är obligatorisk")]
        [DataType(DataType.EmailAddress)]
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
        [Required(ErrorMessage = "Lösenord är obligatorisk")]
        public string Password { get; set; }

        [Display(Name = "Bekräfta lösenord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lösenord ej lika")]
        public string PasswordVerify { get; set; }
    }
}
