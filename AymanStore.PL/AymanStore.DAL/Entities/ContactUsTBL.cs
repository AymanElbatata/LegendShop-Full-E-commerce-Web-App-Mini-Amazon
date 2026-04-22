using AymanStore.DAL.BaseEntity;

namespace AymanStore.DAL.Entities
{
    public class ContactUsTBL : BaseEntity<int>
    {
        public string? Name { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Message { get; set; } = null!;
    }
}