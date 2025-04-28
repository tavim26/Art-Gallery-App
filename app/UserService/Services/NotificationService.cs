using System.Net;
using System.Net.Mail;

namespace UserService.Services
{
    public class NotificationService
    {
        private readonly string smtpHost = "smtp.gmail.com"; // Exemplu Gmail SMTP
        private readonly int smtpPort = 587;
        private readonly string smtpEmail = "your-email@gmail.com"; 
        private readonly string smtpPassword = "your-app-password"; 

        public NotificationService()
        {
        }

        public bool SendEmail(string email, string message)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
                    return false;

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
                    IsBodyHtml = false,
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

        public bool SendSMS(string phone, string message)
        {
            // Simulare cod pentru integrare SMS real (ex: Twilio, Nexmo)
            // Aici pregătim metoda astfel încât să poată fi înlocuită ușor ulterior
            try
            {
                if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(message))
                    return false;

                // Exemplu de trimitere reală s-ar face cu API SMS
                Console.WriteLine($"[SMS REAL - simulare acum] Mesaj SMS către {phone}: {message}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
