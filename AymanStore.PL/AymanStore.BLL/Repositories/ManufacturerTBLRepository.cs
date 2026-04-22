using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ManufacturerTBLRepository : GenericRepository<ManufacturerTBL>, IManufacturerTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public ManufacturerTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
