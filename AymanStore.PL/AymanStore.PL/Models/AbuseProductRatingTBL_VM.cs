using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.Models
{
    public class AbuseProductRatingTBL_VM : BaseEntity<int>
    {
        public int? ProductRatingTBLId { get; set; }
        public virtual ProductRatingTBL? ProductRatingTBL { get; set; }

        [MinLength(5, ErrorMessage = "Minimum Length 5 Charcters")]
        [MaxLength(1000, ErrorMessage = "Maximum Length 1000 Charcter.")]
        [Display(Name = "Issues")]
        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; } = null!;
    }
}
