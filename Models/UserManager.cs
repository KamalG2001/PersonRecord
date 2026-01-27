using PersonRecord.Repos;
using PersonRecord.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace PersonRecord.Models
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ObservableCollection<User> _databaseUsers;

        public UserManager(IUserRepository repository)
        {
            _repository = repository;
            _databaseUsers = new ObservableCollection<User>();
            
            // Load initial data from repository
            var users = _repository.GetAllUsers();
            foreach (var user in users)
            {
                _databaseUsers.Add(user);
            }
        }

        public ObservableCollection<User> GetUsers()
        {
            return _databaseUsers;
        }

        public void AddUser(User user)
        {
            _databaseUsers.Add(user);
            _repository.AddUser(user);
        }

        public void DeleteSelectedUser(User user)
        {
            if (user != null)
            {
                _databaseUsers.Remove(user);
                _repository.DeleteUser(user);
            }
        }

        public void UpdateSelectedUser(User user)
        {
            var existingUser = _databaseUsers.FirstOrDefault(u =>
                u.Name == user.Name && u.Surname == user.Surname);
            
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Surname = user.Surname;
                existingUser.Age = user.Age;
                existingUser.Job = user.Job;
                _repository.UpdateUser(user);
            }
        }
    }
}
