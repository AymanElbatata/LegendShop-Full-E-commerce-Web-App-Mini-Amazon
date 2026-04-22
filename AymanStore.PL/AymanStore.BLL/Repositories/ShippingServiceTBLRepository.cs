using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ShippingServiceTBLRepository : GenericRepository<ShippingServiceTBL>, IShippingServiceTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public ShippingServiceTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
