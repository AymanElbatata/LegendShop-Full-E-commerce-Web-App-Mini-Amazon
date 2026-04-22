using AymanStore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AymanStore.DAL.Entities
{
    public class InvoiceRateTBL : BaseEntity<int>
    {
        public int? VATRate { get; set; } = 0;
        public int? ProfitRate { get; set; } = 0;
    }
}
