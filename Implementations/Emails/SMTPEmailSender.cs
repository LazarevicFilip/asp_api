using Application.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Emails
{
    public class SMTPEmailSender : IEmailSender
    {
        private readonly string _email;
        private readonly string _password;

        public SMTPEmailSender(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public void Send(Email dto)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_email, _password)
            };
            //ugradjena klasa u .net
            var message = new MailMessage(_email, dto.To, dto.Title, dto.Body);
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
