using UserService.Domain;
using UserService.Domain.Contracts;

namespace UserService.Services.Observers
{
    public class UserUpdateNotifier
    {
        private readonly List<IUserObserver> _observers = new();

        public void RegisterObserver(IUserObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify(User user)
        {
            foreach (var observer in _observers)
            {
                observer.OnUserUpdated(user);
            }
        }
    }
}