using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;

namespace AymanStore.BLL.Repositories
{
    public class InvoiceOrderDetailTBLRepository : GenericRepository<InvoiceOrderDetailTBL>, IInvoiceOrderDetailTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public InvoiceOrderDetailTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
