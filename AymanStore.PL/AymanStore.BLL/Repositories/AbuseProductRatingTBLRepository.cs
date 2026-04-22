using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class AbuseProductRatingTBLRepository : GenericRepository<AbuseProductRatingTBL>, IAbuseProductRatingTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public AbuseProductRatingTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
