using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.DTO
{
    public class UserProfileDTO
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        [MinLength(3, ErrorMessage = "First Name must be at least 3 characters")]
        [MaxLength(20, ErrorMessage = "First Name must be at max 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [MinLength(3, ErrorMessage = "Last Name must be at least 3 characters")]
        [MaxLength(20, ErrorMessage = "Last Name must be at max 20 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(100, ErrorMessage = "Last Name must be at max 100 character")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [MaxLength(20, ErrorMessage = "Phone must be at max 20 character")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public int? CountryTBLId { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender")]
        public int? GenderTBLId { get; set; }

        public IEnumerable<SelectListItem> CountryOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> GenderOptions { get; set; } = new List<SelectListItem>();
        public Dictionary<string, string[]>? Errors { get; set; }

    }
}
