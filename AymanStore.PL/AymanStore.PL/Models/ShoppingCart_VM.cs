using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class ShoppingCart_VM
    {
        public OrderTBL_VM OrderTBL_VM { get; set; } = new OrderTBL_VM();
        public List<OrderDetailTBL_VM> OrderDetailTBL_VM { get; set; } = new List<OrderDetailTBL_VM>();
    }


}
