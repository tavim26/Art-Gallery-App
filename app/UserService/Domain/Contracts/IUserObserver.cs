namespace UserService.Domain.Contracts
{
    public interface IUserObserver
    {
        void OnUserUpdated(User user);
    }
}