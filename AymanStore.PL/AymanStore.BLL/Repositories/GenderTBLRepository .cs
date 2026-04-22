using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class GenderTBLRepository : GenericRepository<GenderTBL>, IGenderTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public GenderTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
