using Application.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Emails
{
    public class FakeEmailSender : IEmailSender
    {
        public void Send(Email email)
        {
            Console.WriteLine("Email sent to: " + email.To);
            Console.WriteLine("Title: " + email.Title);
        }
    }
}
