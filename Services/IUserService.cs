using PersonRecord.Models;
using System.Collections.ObjectModel;

namespace PersonRecord.Services
{
    public interface IUserService
    {
        ObservableCollection<User> GetUsers();
        void AddUser(User user);
        void DeleteSelectedUser(User user);
        void UpdateSelectedUser(User user);
    }
}