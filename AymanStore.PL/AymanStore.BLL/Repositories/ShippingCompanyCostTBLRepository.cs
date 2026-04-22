using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ShippingCompanyCostTBLRepository : GenericRepository<ShippingCompanyCostTBL>, IShippingCompanyCostTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public ShippingCompanyCostTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
