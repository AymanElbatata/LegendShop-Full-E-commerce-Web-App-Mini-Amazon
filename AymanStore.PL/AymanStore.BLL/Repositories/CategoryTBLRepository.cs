using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class CategoryTBLRepository : GenericRepository<CategoryTBL>, ICategoryTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public CategoryTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
