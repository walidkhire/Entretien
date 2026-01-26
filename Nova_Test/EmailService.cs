using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Nova_Test
{
    public class EmailService : IEmailService
    {

        public EmailService() { }
        public void SendMail(string to, string sujet, string contenu)
        {
            using var client = new SmtpClient();
            client.Send("contact@veolia.com", to, sujet, contenu);
        }
    }
}
