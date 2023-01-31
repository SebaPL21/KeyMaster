using System.Net.Mail;

namespace KeyMaster.Models
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            string fromMail = "plazasebastian99@gmail.com";
            string fromPassoword = "qnsqciwplcsmrdxb";

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromMail);
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(fromMail, fromPassoword);
            client.Host = "smtpout.secureserver.net";
            //client.Host = "smtpout.secureserver.net";
            client.Port = 80;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;

        }
    }
}
