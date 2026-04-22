using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;
using AymanStore.PL.Models;
using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.DTO
{
    public class ContactForProductErrorDTO : BaseEntity<int>
    {
        public ProductTBL_VM ProductTBL_VM { get; set; } = new ProductTBL_VM();

        [MinLength(5, ErrorMessage = "Minimum Length 5 Charcters")]
        [MaxLength(1000, ErrorMessage = "Maximum Length 1000 Charcter.")]
        [Display(Name = "Your Issue")]
        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; } = null!;
    }
}
