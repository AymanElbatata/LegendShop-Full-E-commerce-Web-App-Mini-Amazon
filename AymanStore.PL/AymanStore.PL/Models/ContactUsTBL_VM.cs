using AymanStore.DAL.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.Models
{
    public class ContactUsTBL_VM : BaseEntity<int>
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters")]
        [MaxLength(50, ErrorMessage = "Name must be at max 50 character")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        [MinLength(5, ErrorMessage = "Minimum Length 5 Charcters")]
        [MaxLength(1000, ErrorMessage = "Maximum Length 1000 Charcter.")]
        [Display(Name = "Your Message")]
        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; } = null!;

    }
}


