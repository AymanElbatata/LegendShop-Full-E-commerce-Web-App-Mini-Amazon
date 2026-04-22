using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class AppErrorTBLRepository : GenericRepository<AppErrorTBL>, IAppErrorTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public AppErrorTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
