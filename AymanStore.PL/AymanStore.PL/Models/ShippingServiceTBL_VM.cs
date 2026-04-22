using AymanStore.DAL.BaseEntity;

namespace AymanStore.PL.Models
{
    public class ShippingServiceTBL_VM : BaseEntity<int>
    {
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string DisplayText => Name + " - " + Description;
    }
}
