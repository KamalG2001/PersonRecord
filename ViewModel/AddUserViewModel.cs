using PersonRecord.Models;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;

namespace PersonRecord.ViewModel
{
    public class AddUserViewModel : INotifyPropertyChanged
    {
        private readonly IUserRepository _repository;

        public AddUserViewModel(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        private string? _name;
        public string? Name 
        { 
            get => _name;
            set 
            { 
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string? _surname;
        public string? Surname 
        { 
            get => _surname;
            set 
            { 
                if (_surname == value) return;
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private int? _age;
        public int? Age 
        { 
            get => _age;
            set 
            { 
                if (_age == value) return;
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private string? _job;
        public string? Job 
        { 
            get => _job;
            set 
            { 
                if (_job == value) return;
                _job = value;
                OnPropertyChanged(nameof(Job));
            }
        }

        public bool CanAddUser { get; set; } = true;

        private RelayCommand _addUserCommand;
        public RelayCommand AddUserCommand => _addUserCommand ?? (_addUserCommand = new RelayCommand(AddUser, () => CanAddUser));

        private void AddUser()
        {
            var newUser = new User() { Name = Name, Surname = Surname, Age = Age, Job = Job };
            _repository.AddUser(newUser);
            UserManager.AddUser(newUser);   
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
