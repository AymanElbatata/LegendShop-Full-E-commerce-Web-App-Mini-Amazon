using AymanStore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AymanStore.DAL.Entities
{
    public class ShippingCompanyCostTBL : BaseEntity<int>
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
