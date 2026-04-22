using AymanStore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AymanStore.DAL.Entities
{
    public class ContactForProductErrorTBL : BaseEntity<int>
    {
        public int? ProductTBLId { get; set; }
        public virtual ProductTBL? ProductTBL { get; set; }

        public string? SenderUserTBLId { get; set; }
        public virtual AppUser? SenderUserTBL { get; set; }

        public string? Message { get; set; } = null!;

        public string? MessageReplyToClient { get; set; } = null!;
        public bool IsSeenMessageReplyToClient { get; set; } = false;
    }
}
