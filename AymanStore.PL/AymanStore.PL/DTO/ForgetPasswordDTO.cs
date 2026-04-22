using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.DTO
{
    public class ForgetPasswordDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
    }
}
