using AymanStore.DAL.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.Models
{
    public class EmailTBL_VM : BaseEntity<int>
    {
        public string? From { get; set; } = null!;
        public string? To { get; set; } = null!;
        public string? Subject { get; set; } = null!;
        public string? Body { get; set; } = null!;
    }
}
