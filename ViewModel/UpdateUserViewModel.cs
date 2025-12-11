using GalaSoft.MvvmLight.Command;
using PersonRecord.Models;
using PersonRecord.Views;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;


namespace PersonRecord.ViewModel
{
    public class UpdateUserViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public string? Job { get; set; }

        private RelayCommand _updateUserCommand;
        public RelayCommand UpdateUserCommand => _updateUserCommand ?? (_updateUserCommand = new RelayCommand(UpdateSelectedUser));
        
        private User? _user;
        public User? User
        {
            get => _user;
            set
            {
                _user = value;
            }
        }

        public UpdateUserViewModel(User? user)
        {
            _user = user;
            if (user != null)
            {
                Name = user.Name;
                Surname = user.Surname;
                Age = user.Age;
                Job = user.Job;
            }
        }

        private void UpdateSelectedUser()
        {
            if (_user != null)
            {
                _user.Name = Name;
                _user.Surname = Surname;
                _user.Age = Age;
                _user.Job = Job;
            }
            var updateUserView = new UpdateUser
            {
                DataContext = new UpdateUserViewModel(_user)
            };
            if (Application.Current.Windows.Count > 0)
            {
                foreach (Window w in Application.Current.Windows)
                {
                    if (w.DataContext == this)
                    {
                        w.Close();
                        break;
                    }
                }
            }
        }
    }
}
