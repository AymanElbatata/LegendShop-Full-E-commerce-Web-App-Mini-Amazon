using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class Orders_VM
    {
        public List<OrderTBL_VM> OrderTBL_VM { get; set; } = new List<OrderTBL_VM>();
        public List<OrderDetailTBL_VM> OrderDetailTBL_VM { get; set; } = new List<OrderDetailTBL_VM>();
    }
}
