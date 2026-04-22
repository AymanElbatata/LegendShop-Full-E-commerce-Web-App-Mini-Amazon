using AymanStore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AymanStore.DAL.Entities
{
    public class AbuseProductRatingTBL : BaseEntity<int>
    {
        public int? ProductRatingTBLId { get; set; }
        public virtual ProductRatingTBL? ProductRatingTBL { get; set; }

        public string? SenderUserTBLId { get; set; }
        public virtual AppUser? SenderUserTBL { get; set; }

        public string? Message { get; set; } = null!;
    }
}
