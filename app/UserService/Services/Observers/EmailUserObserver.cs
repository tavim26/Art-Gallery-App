using UserService.Domain;
using UserService.Domain.Contracts;

namespace UserService.Services.Observers
{
    public class EmailUserObserver : IUserObserver
    {
        private readonly INotificationStrategy _emailStrategy;

        public EmailUserObserver(INotificationStrategy emailStrategy)
        {
            _emailStrategy = emailStrategy;
        }

        public void OnUserUpdated(User user)
        {
            _emailStrategy.Notify(user.Email, "Your account details have been updated.");
        }
    }
}