
using DataAccess.IService;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace DataAccess.Services
{
    public class EmailSender : IEmailSender, IEmailSenderContactUs
    {

        private readonly string _email;
        private readonly string _password;

        public EmailSender(IConfiguration config)
        {
            _email = config["EmailSettings:Email"];
            _password = config["EmailSettings:Password"];
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com",587)
            {
                Credentials = new  NetworkCredential(_email,  _password),
                EnableSsl = true
            };

           
                 return client.SendMailAsync(
                      new MailMessage(_email, email, subject, htmlMessage) { IsBodyHtml = true }
                );

        }

        public Task SendEmailContactUsAsync(string userEmail, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_email, _password),
                EnableSsl = true
            };

            var mail = new MailMessage()
            {
                From = new MailAddress(_email),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mail.To.Add(_email);
            mail.ReplyToList.Add(new MailAddress(userEmail));

            return client.SendMailAsync(mail);
        }
    }
}
