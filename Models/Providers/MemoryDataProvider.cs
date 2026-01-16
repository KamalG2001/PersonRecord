namespace PersonRecord.Models.Providers
{

    public class MemoryDataProvider : IDataProvider
    {
        private readonly List<User> _users = new();

        public string ProviderName => "Memory Provider";

        public List<User> GetAllUsers()
        {
            return new List<User>(_users);
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            if (_users.Any(u => u.Id == user.Id))
                throw new InvalidOperationException($"User with ID {user.Id} already exists");

            _users.Add(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser == null)
                throw new InvalidOperationException($"User with ID {user.Id} not found");

            existingUser.Name = user.Name;
            existingUser.Surname = user.Surname;
            existingUser.Age = user.Age;
            existingUser.Job = user.Job;
        }

        public void DeleteUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var userToDelete = _users.FirstOrDefault(u => u.Id == user.Id);

            if (userToDelete != null)
                _users.Remove(userToDelete);
        }
    }
}