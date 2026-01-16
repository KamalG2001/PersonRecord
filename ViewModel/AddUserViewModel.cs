using PersonRecord.Models;
using PersonRecord.Models.Providers;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;

namespace PersonRecord.ViewModel
{
    public class AddUserViewModel : INotifyPropertyChanged
    {
        private readonly IDataProvider _dataProvider;

        public AddUserViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
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

        public bool CanAddUser => !string.IsNullOrWhiteSpace(Name) && 
            !string.IsNullOrWhiteSpace(Surname) && 
            Age.HasValue && 
            !string.IsNullOrWhiteSpace(Job);

        private RelayCommand _addUserCommand;
        public RelayCommand AddUserCommand => _addUserCommand ?? (_addUserCommand = new RelayCommand(AddUser, () => CanAddUser));

        private void AddUser()
        {
            if (!ValidateInput())
                return;

                var newUser = new User { Name = Name, Surname = Surname, Age = Age, Job = Job };
                _dataProvider.AddUser(newUser);
                ClearFields();
                CloseWindow();
           
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Surname))
            {
                MessageBox.Show("Please enter a surname.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!Age.HasValue || Age <= 0)
            {
                MessageBox.Show("Please enter a valid age.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Job))
            {
                MessageBox.Show("Please enter a job.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void ClearFields()
        {
            Name = null;
            Surname = null;
            Age = null;
            Job = null;
        }

        private void CloseWindow()
        {
            Application.Current.Windows.OfType<Views.AddUser>().FirstOrDefault()?.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
