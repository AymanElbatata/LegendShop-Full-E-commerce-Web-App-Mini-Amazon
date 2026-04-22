using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class OrderDetailTBL_VM : BaseEntity<int>
    {
        public int? OrderTBLId { get; set; }
        public virtual OrderTBL? OrderTBL { get; set; }

        public int? ProductTBLId { get; set; }
        public virtual ProductTBL? ProductTBL { get; set; }

        public int? ShippingStatusTBLId { get; set; }
        public virtual ShippingStatusTBL? ShippingStatusTBL { get; set; }

        public int? ShippingCompanyCostTBLId { get; set; }
        public virtual ShippingCompanyCostTBL? ShippingCompanyCostTBL { get; set; }

        public string? ShippingCompanyTRKCode { get; set; } = null!;

        public string? SupplierAddress { get; set; } = null!;
        public string? SupplierEmail { get; set; } = null!;
        public string? SupplierPhones { get; set; } = null!;

        public bool IsUserWroteReview { get; set; } = false;

        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public int? ShippingCost { get; set; }

    }
}
