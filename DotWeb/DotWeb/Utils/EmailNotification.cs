using System.Net.Mail;
using DotWeb.Repositories;

namespace DotWeb.Utils
{
    public class EmailNotification
    {
        public static void GenerateEmail(string To, string Subject, string Message)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(To);
            mail.From = new MailAddress(EmailRepository.getSMTPUsername());
            mail.Subject = Subject;
            mail.Body = Message;
            //mail.IsBodyHtml = true;

            SmtpClient SmtpMail = new SmtpClient(EmailRepository.getSMTPHost(), EmailRepository.getSMTPPort());
            SmtpMail.EnableSsl = EmailRepository.SMTPIsUseSSL();
            SmtpMail.UseDefaultCredentials = false;
            SmtpMail.Credentials = new System.Net.NetworkCredential(EmailRepository.getSMTPUsername(), EmailRepository.getSMTPPassword());
            SmtpMail.Send(mail);            

            #region body
            //ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010);
            ////service.AutodiscoverUrl("itm-department.pool-id@daimler.com");

            ////service.Url = new Uri("https://outlook.office365.com/ews/Exchange.asmx");
            //service.Url = new Uri("https://oa.wp.corpshared.net/ews/Exchange.asmx");

            //service.UseDefaultCredentials = false;
            //service.Credentials = new WebCredentials("itm-department.pool-id@daimler.com", "Jakarta123");


            //EmailMessage message = new EmailMessage(service);
            //message.Subject = Subject;
            //message.Body = Message;
            //message.ToRecipients.Add(To);
            //message.Save();

            //message.SendAndSaveCopy();

            //oMsg.IsBodyHtml = true;

            //Message = "Dear " + To + ", <br />";
            //Message += "------------------------------------------------------------------------------------------------ <br/> <br/>";

            ////Project Schedule Monitoring <bulan tahun> untuk project <nama project>, telah di <action> oleh <Project Administrator>

            //Message += "<b> Project Schedule Monitoring " + DateTime.Now.ToString("MMM") + " - " + DateTime.Now.Year + " untuk project " + namaproject + " telah di Submit oleh " + ProjectAdministrator + " </b>";
            //Message += " <br /> <br /> <br />";

            //Message += "<i> Mohon bantuannya untuk melakukan review update progress project tersebut dengan menekan link berikut : <br /> <br /> ";
            ////Message += " <a href=\"" + _URL + "\">Task </a> </i> <br /> <br />";

            //Message += "<i>Hormat Kami, <br /> ";
            //Message += "Administratot ";
            //Message += "</i> ";                       

            //SmtpMail.Host = webapp.OutboundMailServiceInstance.Parent.Name;//GetSMTPHostName(); //"smtp.sinarmasland.com";
            #endregion
        }
    }
}
