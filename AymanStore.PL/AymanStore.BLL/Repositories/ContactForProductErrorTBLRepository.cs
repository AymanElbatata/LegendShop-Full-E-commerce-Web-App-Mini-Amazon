using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ContactForProductErrorTBLRepository : GenericRepository<ContactForProductErrorTBL>, IContactForProductErrorTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public ContactForProductErrorTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
