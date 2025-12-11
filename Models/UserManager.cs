using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;

namespace PersonRecord.Models
{
    public class UserManager
    {
        public static ObservableCollection<User> _DatabaseUsers = new ObservableCollection<User>() 
        {
           //new User(){ Name = "Vagif", Surname = "Mammadov", Age = 25, Job = "Accountant" },
           //new User(){ Name = "Akif", Surname = "Valizada", Age = 32, Job = "Trainer" },
           //new User(){ Name = "Rustam", Surname = "Talibov", Age = 44, Job ="Businessman" },
           //new User(){ Name = "Ayaz", Surname = "Guliyev", Age = 20, Job = "------" }
        };
        

        public static ObservableCollection<User> GetUsers()
        {
            return _DatabaseUsers;
        }

        public static ObservableCollection<User> SetUsers(User user)
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

        public static void DeleteUser(User user) 
        {
      
            int index = -1;
            for (int i = 0; i < _DatabaseUsers.Count; i++)
            {
                if (_DatabaseUsers[i].Name != null && _DatabaseUsers[i].Surname != null &&
                    _DatabaseUsers[i].Name.Equals(user.Name, StringComparison.OrdinalIgnoreCase) &&
                    _DatabaseUsers[i].Surname.Equals(user.Surname, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                _DatabaseUsers.RemoveAt(index);
            }
        }
    }
}
