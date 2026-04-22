using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class CheckOut_VM
    {
        public OrderTBL_VM OrderTBL_VM { get; set; } = new OrderTBL_VM();
        public List<CountryTBL_VM> CountryTBL_VM { get; set; } = new List<CountryTBL_VM>();
        public List<ShippingServiceTBL_VM> ShippingServiceTBL_VM { get; set; } = new List<ShippingServiceTBL_VM>();
        public int? TotalItemsOfOrder { get; set; }
        public int? TotalAmountOfOrder { get; set; }
    }



}
