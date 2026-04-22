using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;

namespace AymanStore.DAL.Entities
{
    public class ProductRatingTBL : BaseEntity<int>
    {
        public int? ProductTBLId { get; set; }
        public virtual ProductTBL? ProductTBL { get; set; }

        public string? AppUserWhoRatedId { get; set; }
        public virtual AppUser? AppUserWhoRated { get; set; }

        public int Stars { get; set; }
        public string? ReviewSubject { get; set; } = null!;
        public string? ReviewComment { get; set; } = null!;

        public bool IsHelpful { get; set; } = false;

    }
}
