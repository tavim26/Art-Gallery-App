using System.Net;
using System.Net.Mail;
using UserService.Domain.Contracts;

namespace UserService.Services.Notifications
{
    public class EmailNotificationStrategy : INotificationStrategy
    {
        private readonly string smtpEmail;
        private readonly string smtpPassword;
        private readonly string smtpHost = "smtp.gmail.com";
        private readonly int smtpPort = 587;

        public EmailNotificationStrategy(IConfiguration config)
        {
            smtpEmail = config["Smtp:Email"];
            smtpPassword = config["Smtp:Password"];
        }

        public bool Notify(string email, string message)
        {
            try
            {
                var client = new SmtpClient(smtpHost)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(smtpEmail, smtpPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpEmail),
                    Subject = "Notificare Platforma Galerii de Artă",
                    Body = message,
                    IsBodyHtml = false
                };
                mailMessage.To.Add(email);

                client.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
