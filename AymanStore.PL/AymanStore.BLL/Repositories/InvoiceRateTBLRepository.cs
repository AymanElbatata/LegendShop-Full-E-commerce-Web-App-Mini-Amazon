using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class InvoiceRateTBLRepository : GenericRepository<InvoiceRateTBL>, IInvoiceRateTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public InvoiceRateTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
