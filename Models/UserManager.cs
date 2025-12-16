using System.Collections.ObjectModel;

namespace PersonRecord.Models
{
    public class UserManager
    {
        public static ObservableCollection<User> _DatabaseUsers = new ObservableCollection<User>() 
        {

        };
        

        public static ObservableCollection<User> GetUsers()
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
    }
}
