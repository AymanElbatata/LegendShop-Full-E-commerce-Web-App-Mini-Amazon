using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class CountryTBLRepository : GenericRepository<CountryTBL>, ICountryTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public CountryTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
