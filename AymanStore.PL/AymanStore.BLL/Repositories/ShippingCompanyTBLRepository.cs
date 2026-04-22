using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ShippingCompanyTBLRepository : GenericRepository<ShippingCompanyTBL>, IShippingCompanyTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public ShippingCompanyTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
