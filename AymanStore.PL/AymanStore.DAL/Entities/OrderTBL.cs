using AymanStore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AymanStore.DAL.Entities
{
    public class OrderTBL : BaseEntity<int>
    {
        public string? OrderByUserTBLId { get; set; }
        public virtual AppUser? OrderByUserTBL { get; set; }

        public int? CountryTBLId { get; set; }
        public virtual CountryTBL? CountryTBL { get; set; }

        public int? ShippingServiceTBLId { get; set; }
        public virtual ShippingServiceTBL? ShippingServiceTBL { get; set; }

        public int? ShippingStatusTBLId { get; set; }
        public virtual ShippingStatusTBL? ShippingStatusTBL { get; set; }

        //public int? ShippingCompanyTBLId { get; set; }
        //public virtual ShippingCompanyTBL? ShippingCompanyTBL { get; set; }


        public string? ClientName { get; set; } = null!;
        public string? ClientEmail { get; set; } = null!;
        public string? ClientPhones { get; set; } = null!;
        public string? ClientAddress { get; set; } = null!;
        public string? ClientNotes { get; set; } = null!;

        public bool IsСurrentOrder { get; set; } = false;
        public string? HashCode { get; set; } = null!;

        public string? TrackingShippingCode { get; set; } = null!;
        public string? TrackingShippingUrl { get; set; } = null!;

        public DateTime? DateOfSubmit { get; set; }


        public int? TotalAmount { get; set; } = 0;

    }
}
