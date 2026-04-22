using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Net;
using System.Net.Mail;

namespace AymanStore.BLL.Repositories
{
    public class OrderTBLRepository : GenericRepository<OrderTBL>, IOrderTBLRepository
    {
        private readonly AymanStoreDbContext _context;

        public OrderTBLRepository(AymanStoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
