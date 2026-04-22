using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ShippingStatusTBLRepository : GenericRepository<ShippingStatusTBL>, IShippingStatusTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public ShippingStatusTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
