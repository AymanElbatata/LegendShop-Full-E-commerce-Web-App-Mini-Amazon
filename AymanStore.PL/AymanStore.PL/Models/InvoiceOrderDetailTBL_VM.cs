using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class InvoiceOrderDetailTBL_VM : BaseEntity<int>
    {
        public int? OrderDetailTBLId { get; set; }
        public virtual OrderDetailTBL? OrderDetailTBL { get; set; }

        public bool IsSupplierReceivedMoney { get; set; } = false;

        public int? SupplierAmount { get; set; }
        public int? VATAmount { get; set; }
        public int? ProfitAmount { get; set; }
        public int? ShippingAmount { get; set; }
    }
}
