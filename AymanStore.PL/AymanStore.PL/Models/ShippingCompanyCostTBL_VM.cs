using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class ShippingCompanyCostTBL_VM : BaseEntity<int>
    {
        public int? ShippingCompanyTBLId { get; set; }
        public virtual ShippingCompanyTBL? ShippingCompanyTBL { get; set; }

        public int? CountryTBLSendFromId { get; set; }
        public virtual CountryTBL? CountryTBLSendFrom { get; set; }

        public int? CountryTBLSendToId { get; set; }
        public virtual CountryTBL? CountryTBLSendTo { get; set; }

        public int? Cost { get; set; }

    }
}
