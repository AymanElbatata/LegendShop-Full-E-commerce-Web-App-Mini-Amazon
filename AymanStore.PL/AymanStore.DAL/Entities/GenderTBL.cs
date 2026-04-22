using AymanStore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AymanStore.DAL.Entities
{
    public class GenderTBL : BaseEntity<int>
    {
        public string? Name { get; set; } = null!;
    }
}
