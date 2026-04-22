using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.Models
{
    public class ContactForProductErrorTBL_VM : BaseEntity<int>
    {
        public int? ProductTBLId { get; set; }
        public virtual ProductTBL? ProductTBL { get; set; }

        public string? SenderUserTBLId { get; set; }
        public virtual AppUser? SenderUserTBL { get; set; }

        [MinLength(5, ErrorMessage = "Minimum Length 5 Charcters")]
        [MaxLength(1000, ErrorMessage = "Maximum Length 1000 Charcter.")]
        [Display(Name = "Your Problem")]
        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; } = null!;

        public string? MessageReplyToClient { get; set; } = null!;
        public bool IsSeenMessageReplyToClient { get; set; } = false;

    }
}
