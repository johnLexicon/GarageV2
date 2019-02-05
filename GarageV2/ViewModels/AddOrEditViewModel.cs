using GarageV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.ViewModels
{
    public class AddOrEditViewModel
    {
        private string _regNo;

        public int Id { get; set; }

        [Required]
        [Remote(action: "CheckIfRegNoExists", controller: "ParkedVehicles", AdditionalFields = nameof(Id))]
        [RegularExpression(@"^[a-zA-Z]{3}\d{3}$", ErrorMessage = "Fel format för reg-nummer")]
        [Display(Name = "Reg-nummer")]
        public string RegNo { get { return _regNo; } set { _regNo = value.ToUpper(); } }

        [Display(Name = "Fordonstyp")]
        public IEnumerable<VehicleType> ParkedVehicleTypes { get; set; }

        [Display(Name = "Färg")]
        [StringLength(8, MinimumLength = 3, ErrorMessage = "Färg ska vara en sträng mellan 3 till 8 tecken")]
        public string Color { get; set; }

        [Display(Name = "Märke")]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        public string Model { get; set; }

        [Display(Name = "Antal däck")]
        [Range(0, 18, ErrorMessage = "Kan endast vara 0 till 18 däck")]
        public int NoWheels { get; set; }

        [Display(Name = "Incheckning")]
        public DateTime CheckIn { get; set; }

        public bool AlreadyParked { get; set; }

        public List<SelectListItem> GetParkeVehicleTypes()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach(var item in ParkedVehicleTypes)
            {
                items.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            return items;
        }

    }
}
