using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class ProductTBL_VM : BaseEntity<int>
    {
        public string? SupplierTBLId { get; set; }
        public virtual AppUser? SupplierTBL { get; set; }

        public int? SubCategoryTBLId { get; set; }
        public virtual SubCategoryTBL? SubCategoryTBL { get; set; }

        public int? ManufacturerTBLId { get; set; }
        public virtual ManufacturerTBL? ManufacturerTBL { get; set; }

        public int? CountryTBLPlaceId { get; set; }
        public virtual CountryTBL? CountryTBLPlace { get; set; }


        public string? Name { get; set; } = null!;
        public string? Image { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public int? SellingPrice { get; set; } = 0;
        public int? PurchasingPrice { get; set; } = 0;
        public int? Quantity { get; set; } = 0;

        public string? Brand { get; set; } = null!;
        public string? Model { get; set; } = null!;
        public string? Material { get; set; } = null!;
        public string? Color { get; set; } = null!;
        public string? Weight { get; set; } = null!;
        public string? Size { get; set; } = null!;
        public string? Barcode { get; set; } = null!;
        public DateTime? ValidFrom { get; set; } = DateTime.Now;

        public bool IsPublished { get; set; } = false;

    }
}
