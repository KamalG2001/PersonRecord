using System.Collections.ObjectModel;
using System.Linq;
using Windows.System;

namespace PersonRecord.Models
{
    public class UserManager
    {
        public static List<User> _DatabaseUsers = new List<User>();
           
        public static List<User> GetUsers()
        {
            return _DatabaseUsers;
        }

        public static void AddUser(User user)
        {
            _DatabaseUsers.Add(user);
        }

        public static void DeleteSelectedUser(User user)
        {
            if (user != null)
                _DatabaseUsers.Remove(user);
        }

        public static void UpdateSelectedUser(User user)
        {
            var existingUser = _DatabaseUsers.FirstOrDefault(u =>
                u.Name == user.Name && u.Surname == user.Surname);
            
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Surname = user.Surname;
                existingUser.Age = user.Age;
                existingUser.Job = user.Job;
            }
        }
    }
}
