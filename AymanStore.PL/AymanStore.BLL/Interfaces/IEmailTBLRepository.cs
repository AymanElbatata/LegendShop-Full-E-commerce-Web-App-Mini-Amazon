using AymanStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AymanStore.BLL.Interfaces
{
    public interface IEmailTBLRepository : IGenericRepository<EmailTBL>
    {
        Task SendEmail(EmailTBL email);
        Task SendEmailAsync(EmailTBL emails);
        Task SendEmailAsync(EmailTBL emails, int SecondType = 0, List<string>? ccEmails = null);

    }
}
