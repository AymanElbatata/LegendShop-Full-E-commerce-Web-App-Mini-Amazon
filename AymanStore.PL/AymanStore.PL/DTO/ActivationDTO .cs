using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.DTO
{
    public class ActivationDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Activation Code is required")]
        public string? ActivationCode { get; set; }

    }
}
