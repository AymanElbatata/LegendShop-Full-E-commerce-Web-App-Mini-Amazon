using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ProductPhotoTBLRepository : GenericRepository<ProductPhotoTBL>, IProductPhotoTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public ProductPhotoTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
