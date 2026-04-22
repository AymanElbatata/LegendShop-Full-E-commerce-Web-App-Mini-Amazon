using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;

namespace AymanStore.DAL.Entities
{
    public class InvoiceOrderDetailTBL : BaseEntity<int>
    {
        public int? OrderDetailTBLId { get; set; }
        public virtual OrderDetailTBL? OrderDetailTBL { get; set; }

        public bool IsSupplierReceivedMoney{ get; set; } = false;

        public int? SupplierAmount { get; set; }
        public int? VATAmount { get; set; }
        public int? ProfitAmount { get; set; }
        public int? ShippingAmount { get; set; }

    }
}
