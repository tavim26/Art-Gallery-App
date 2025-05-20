namespace UserService.Domain.Contracts
{
    public interface INotificationStrategy
    {
        bool Notify(string destination, string message);
    }
}