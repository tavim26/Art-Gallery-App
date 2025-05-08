using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Microsoft.Extensions.Configuration;

namespace UserService.Services.Notifications
{
    public class SmsNotificationStrategy : INotificationStrategy
    {
        private readonly string accountSid;
        private readonly string authToken;
        private readonly string messagingServiceSid;

        public SmsNotificationStrategy(IConfiguration config)
        {
            accountSid = config["Twilio:AccountSid"];
            authToken = config["Twilio:AuthToken"];
            messagingServiceSid = config["Twilio:MessagingServiceSid"];
        }

        public bool Notify(string phone, string message)
        {
            try
            {
                TwilioClient.Init(accountSid, authToken);

                if (phone.StartsWith("0"))
                {
                    phone = "+40" + phone.Substring(1); 
                }

                var result = MessageResource.Create(
                    to: new PhoneNumber(phone),
                    messagingServiceSid: messagingServiceSid,
                    body: message
                );



                return result.ErrorCode == null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }

}