using AymanStore.DAL.BaseEntity;

namespace AymanStore.PL.Models
{
    public class GenderTBL_VM : BaseEntity<int>
    {
        public string? Name { get; set; } = null!;
    }
}
