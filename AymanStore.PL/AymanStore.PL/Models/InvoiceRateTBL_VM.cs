using AymanStore.DAL.BaseEntity;

namespace AymanStore.PL.Models
{
    public class InvoiceRateTBL_VM : BaseEntity<int>
    {
        public int? VATRate { get; set; } = 0;
        public int? ProfitRate { get; set; } = 0;
    }
}
