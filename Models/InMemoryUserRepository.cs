using PersonRecord.Models;
using System.Collections.ObjectModel;

namespace PersonRecord.FileReader
{
    public class InMemoryUserRepository : IUserRepository
    {
        private List<User> _users;

        public InMemoryUserRepository()
        {
            _users = new List<User>()
            {
                new User(){ Name = "Vagif", Surname = "Mammadov", Age = 25, Job = "Accountant" },
                new User(){ Name = "Akif", Surname = "Valizada", Age = 32, Job = "Trainer" },
                new User(){ Name = "Rustam", Surname = "Talibov", Age = 44, Job ="Businessman" },
                new User(){ Name = "Ayaz", Surname = "Guliyev", Age = 20, Job = "------" }
            };
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            _users.Add(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var existingUser = _users.FirstOrDefault(u => 
                u.Name == user.Name && u.Surname == user.Surname);

            if (existingUser == null)
                throw new InvalidOperationException($"User {user.Name} {user.Surname} not found");

            existingUser.Name = user.Name;
            existingUser.Surname = user.Surname;
            existingUser.Age = user.Age;
            existingUser.Job = user.Job;
        }

        public void DeleteUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var userToDelete = _users.FirstOrDefault(u => 
                u.Name == user.Name && u.Surname == user.Surname);

            if (userToDelete != null)
                _users.Remove(userToDelete);
        }
    }
}