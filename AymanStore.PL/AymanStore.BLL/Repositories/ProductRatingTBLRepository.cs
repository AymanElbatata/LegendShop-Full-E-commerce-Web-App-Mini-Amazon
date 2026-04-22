using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ProductRatingTBLRepository : GenericRepository<ProductRatingTBL>, IProductRatingTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public ProductRatingTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
