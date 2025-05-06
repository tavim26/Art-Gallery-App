namespace UserService.Services.Notifications
{
    public class NotificationService
    {
        private readonly INotificationStrategy _emailStrategy;
        private readonly INotificationStrategy _smsStrategy;

        public NotificationService(INotificationStrategy emailStrategy, INotificationStrategy smsStrategy)
        {
            _emailStrategy = emailStrategy;
            _smsStrategy = smsStrategy;
        }

        public bool NotifyByEmail(string email, string message)
        {
            return _emailStrategy.Notify(email, message);
        }

        public bool NotifyBySms(string phone, string message)
        {
            return _smsStrategy.Notify(phone, message);
        }
    }
}