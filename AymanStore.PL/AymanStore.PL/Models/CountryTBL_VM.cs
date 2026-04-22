using AymanStore.DAL.BaseEntity;

namespace AymanStore.PL.Models
{
    public class CountryTBL_VM : BaseEntity<int>
    {
        public string? Name { get; set; } = null!;
    }
}
