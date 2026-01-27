using PersonRecord.Models;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using PersonRecord.Repos;
using PersonRecord.Services;

namespace PersonRecord.ViewModel
{
    public class EditUserViewModel : INotifyPropertyChanged
    {
        private User? _user;
        private readonly IUserRepository _repository;
        private readonly IUserService? _userService;
        private readonly bool _isEditMode;
        
        // Adds user
        public EditUserViewModel(IUserRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
            _isEditMode = false;
            _user = null;
        }

        // Edits user
        public EditUserViewModel(User user, IUserRepository repository)
        {
            _user = user;
            _repository = repository;
            _userService = null;
            _isEditMode = true;
        }

        private string? _name;
        public string? Name
        {
            get => _isEditMode ? _user?.Name : _name;
            set
            {
                if (_isEditMode)
                {
                    if (_user?.Name == value) return;
                    if (_user != null) _user.Name = value;
                }
                else
                {
                    if (_name == value) return;
                    _name = value;
                }
                OnPropertyChanged(nameof(Name));
            }
        }

        private string? _surname;
        public string? Surname
        {
            get => _isEditMode ? _user?.Surname : _surname;
            set
            {
                if (_isEditMode)
                {
                    if (_user?.Surname == value) return;
                    if (_user != null) _user.Surname = value;
                }
                else
                {
                    if (_surname == value) return;
                    _surname = value;
                }
                OnPropertyChanged(nameof(Surname));
            }
        }

        private int? _age;
        public int? Age
        {
            get => _isEditMode ? _user?.Age : _age;
            set
            {
                if (_isEditMode)
                {
                    if (_user?.Age == value) return;
                    if (_user != null) _user.Age = value;
                }
                else
                {
                    if (_age == value) return;
                    _age = value;
                }
                OnPropertyChanged(nameof(Age));
            }
        }

        private string? _job;
        public string? Job
        {
            get => _isEditMode ? _user?.Job : _job;
            set
            {
                if (_isEditMode)
                {
                    if (_user?.Job == value) return;
                    if (_user != null) _user.Job = value;
                }
                else
                {
                    if (_job == value) return;
                    _job = value;
                }
                OnPropertyChanged(nameof(Job));
            }
        }

        public bool CanAddUser { get; set; } = true;
        
        private RelayCommand _addUserCommand;
        public RelayCommand AddUserCommand => _addUserCommand ??= new RelayCommand(EditUser, () => CanAddUser);

        private void EditUser()
        {
            if (_isEditMode)
            {
                if (_user != null)
                {
                    _repository.UpdateUser(_user);
                }
            }
            else
            {
                var newUser = new User
                {
                    Name = _name,
                    Surname = _surname,
                    Age = _age,
                    Job = _job
                };
                
                // Use IUserService to add user so it appears in ObservableCollection
                _userService?.AddUser(newUser);
            }

            CloseWindow();
        }

        private void CloseWindow()
        {
            Application.Current.Windows.OfType<Window>()
                .FirstOrDefault(w => w.IsActive)?.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}