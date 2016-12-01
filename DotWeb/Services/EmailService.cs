using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Services
{
    public class EmailService : IIdentityMessageService
    {
        public string Sender { get; set; }

        public Task SendAsync(IdentityMessage message)
        {
            var mail = new MailMessage(from: Sender, to: message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            //var smtpClient = new SmtpClient();
            //smtpClient.Host = ConfigurationManager.AppSettings["SmtpServer"];
            //smtpClient.Port = Int32.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            //smtpClient.EnableSsl = true;
            //smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUser"], ConfigurationManager.AppSettings["SmtpPassword"]);

            //smtpClient.SendAsync(mail, null);

            return Task.FromResult(0);
        }
    }
}
