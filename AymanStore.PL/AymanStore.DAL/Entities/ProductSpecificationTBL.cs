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
    public class ProductSpecificationTBL : BaseEntity<int>
    {
        public int? ProductTBLId { get; set; }
        public virtual ProductTBL? ProductTBL { get; set; }

        public string? Name { get; set; } = null!;
    }
}
