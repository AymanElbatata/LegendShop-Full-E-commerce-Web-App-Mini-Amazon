using AymanStore.BLL.Interfaces;
using AymanStore.BLL.IRepositories;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ContactUsRepository : GenericRepository<ContactUsTBL> , IContactUsRepository
    {
        private readonly AymanStoreDbContext _context;

        public ContactUsRepository(AymanStoreDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
