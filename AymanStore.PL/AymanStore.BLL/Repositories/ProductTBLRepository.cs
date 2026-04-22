using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ProductTBLRepository : GenericRepository<ProductTBL>, IProductTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public ProductTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
