using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class SubCategoryTBLRepository : GenericRepository<SubCategoryTBL>, ISubCategoryTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public SubCategoryTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
