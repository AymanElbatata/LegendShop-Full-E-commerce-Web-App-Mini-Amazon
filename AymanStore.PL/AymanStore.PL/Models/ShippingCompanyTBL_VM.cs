using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class ShippingCompanyTBL_VM : BaseEntity<int>
    {
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
    }
}
