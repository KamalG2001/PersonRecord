using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRecord.Models
{
    public class UserManager
    {
        public static ObservableCollection<User> _DatabaseUsers = new ObservableCollection<User>() 
        {
           new User(){ Name = "Vagif", Surname = "Mammadov", Age = 25, Job = "Accountant" },
           new User(){ Name = "Akif", Surname = "Valizada", Age = 32, Job = "Trainer" },
           new User(){ Name = "Rustam", Surname = "Talibov", Age = 44, Job ="Businessman" },
           new User(){ Name = "Ayaz", Surname = "Guliyev", Age = 20, Job = "------" }
        };

        public static ObservableCollection<User> GetUsers()
        {
            return _DatabaseUsers;
        }

        public static void AddUser(User user)
        {
            _DatabaseUsers.Add(user);

        }

    }
}
