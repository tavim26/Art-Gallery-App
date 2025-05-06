namespace UserService.Services.Notifications
{
    public interface INotificationStrategy
    {
        bool Notify(string destination, string message);
    }
}