using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class ProductSpecificationTBLRepository : GenericRepository<ProductSpecificationTBL>, IProductSpecificationTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public ProductSpecificationTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
