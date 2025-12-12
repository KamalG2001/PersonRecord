using GalaSoft.MvvmLight.Command;
using PersonRecord.Models;
using PersonRecord.Views;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;


namespace PersonRecord.ViewModel
{
    public class UpdateUserViewModel : INotifyPropertyChanged
    {
        public event System.Action? UserUpdated;

        private string? _name;
        public string? Name { get => _name; set { if (_name == value) return; _name = value; OnPropertyChanged(nameof(Name)); } }

        private string? _surname;
        public string? Surname { get => _surname; set { if (_surname == value) return; _surname = value; OnPropertyChanged(nameof(Surname)); } }

        private int? _age;
        public int? Age { get => _age; set { if (_age == value) return; _age = value; OnPropertyChanged(nameof(Age)); } }

        private string? _job;
        public string? Job { get => _job; set { if (_job == value) return; _job = value; OnPropertyChanged(nameof(Job)); } }


        private RelayCommand _updateUserCommand;
        public RelayCommand UpdateUserCommand => _updateUserCommand ?? (_updateUserCommand = new RelayCommand(UpdateSelectedUser));
        
        private User? _user;
        public User? User
        {
            get => _user;
            set
            {
                if (_user == value) return;
                _user = value;
                OnPropertyChanged(nameof(User));
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

                // Notify that user has been updated
                UserUpdated?.Invoke();
            }

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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
