using UserService.Domain;
using UserService.Domain.Contracts;

namespace UserService.Services.Observers
{
    public class SmsUserObserver : IUserObserver
    {
        private readonly INotificationStrategy _smsStrategy;

        public SmsUserObserver(INotificationStrategy smsStrategy)
        {
            _smsStrategy = smsStrategy;
        }

        public void OnUserUpdated(User user)
        {
            _smsStrategy.Notify(user.Phone, "Your account details have been updated.");
        }
    }
}
