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
    public class SubCategoryTBL : BaseEntity<int>
    {
        public int? CategoryTBLId { get; set; }
        public virtual CategoryTBL? CategoryTBL { get; set; }

        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
    }
}
