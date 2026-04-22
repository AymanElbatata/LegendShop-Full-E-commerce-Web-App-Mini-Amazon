using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class SubCategoryTBL_VM : BaseEntity<int>
    {
        public int? CategoryTBLId { get; set; }
        public virtual CategoryTBL? CategoryTBL { get; set; }

        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;

    }
}
