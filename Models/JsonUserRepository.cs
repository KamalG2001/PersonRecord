using System.IO;
using System.Text.Json;

namespace PersonRecord.Models
{
    public class JsonUserRepository : IUserRepository
    {
        private readonly string _filePath;
        private List<User> _users;

        public JsonUserRepository(string filePath = "JsonReader/Users.json")
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            _users = new List<User>();
        }

        public List<User> GetAllUsers()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine($"JSON file not found at: {_filePath}");
                    return new List<User>();
                }

                var jsonData = File.ReadAllText(_filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                _users = JsonSerializer.Deserialize<List<User>>(jsonData, options) ?? new List<User>();
                return _users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON: {ex.Message}");
                return new List<User>();
            }
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            _users.Add(user);
            SaveToJson();
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

            SaveToJson();
        }

        public void DeleteUser(User user)
        {
            var userToDelete = _users.FirstOrDefault(u => 
                u.Name == user.Name && u.Surname == user.Surname);

            _users.Remove(userToDelete);
            SaveToJson();
        }

        private void SaveToJson()
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            

            var jsonData = JsonSerializer.Serialize(_users, options); 
            File.WriteAllText(_filePath, jsonData);

        }
    }
}