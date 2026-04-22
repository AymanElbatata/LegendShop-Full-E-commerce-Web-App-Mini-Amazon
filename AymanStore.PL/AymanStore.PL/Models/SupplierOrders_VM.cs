using Microsoft.AspNetCore.Mvc.Rendering;

namespace AymanStore.PL.Models
{
    public class SupplierOrders_VM
    {
        public IEnumerable<SelectListItem> ShippingStatusOptions { get; set; } = new List<SelectListItem>();
        public List<OrderDetailTBL_VM> OrderDetailTBL_VM { get; set; } = new List<OrderDetailTBL_VM>();
    }
}
