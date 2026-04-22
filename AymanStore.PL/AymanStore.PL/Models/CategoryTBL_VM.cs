using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.Models
{
    public class CategoryTBL_VM : BaseEntity<int>
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; } = null!;
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; } = null!;
    }
}
