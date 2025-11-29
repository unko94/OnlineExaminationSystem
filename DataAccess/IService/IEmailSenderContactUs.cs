using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IService
{
    public interface IEmailSenderContactUs
    {
        public Task SendEmailContactUsAsync(string userEmail, string subject, string htmlMessage);
    }
}
