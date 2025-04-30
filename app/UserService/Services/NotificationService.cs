using System.Net;
using System.Net.Mail;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace UserService.Services
{
    public class NotificationService
    {
        private readonly string smtpHost = "smtp.gmail.com";
        private readonly int smtpPort = 587;

        private readonly string smtpEmail;
        private readonly string smtpPassword;

        private readonly string twilioAccountSid;
        private readonly string twilioAuthToken;
        private readonly string messagingServiceSid;

        public NotificationService(IConfiguration config)
        {
            smtpEmail = config["Smtp:Email"];
            smtpPassword = config["Smtp:Password"];
            twilioAccountSid = config["Twilio:AccountSid"];
            twilioAuthToken = config["Twilio:AuthToken"];
            messagingServiceSid = config["Twilio:MessagingServiceSid"];
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
            try
            {
                if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(message))
                    return false;

                TwilioClient.Init(twilioAccountSid, twilioAuthToken);

                var result = MessageResource.Create(
                    to: new PhoneNumber(phone),
                    messagingServiceSid: messagingServiceSid,
                    body: message
                );

                return result.ErrorCode == null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare Twilio: {ex.Message}");
                return false;
            }
        }
    }
}
