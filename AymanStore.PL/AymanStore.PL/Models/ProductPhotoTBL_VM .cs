using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class ProductPhotoTBL_VM : BaseEntity<int>
    {
        public int? ProductTBLId { get; set; }
        public virtual ProductTBL? ProductTBL { get; set; }
        public string? Image { get; set; } = null!;

    }
}
