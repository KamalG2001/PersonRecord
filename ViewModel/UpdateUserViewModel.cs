using GalaSoft.MvvmLight.Command;
using PersonRecord.Models;
using System.ComponentModel;
using System.Windows;

namespace PersonRecord.ViewModel
{
    public class UpdateUserViewModel : INotifyPropertyChanged
    {
        private User _user;
        private readonly IUserRepository _repository;

        private RelayCommand? _saveCommand;
        public RelayCommand SaveCommand =>
            _saveCommand ??= new RelayCommand(SaveUser);

        private RelayCommand? _cancelCommand;
        public RelayCommand CancelCommand =>
            _cancelCommand ??= new RelayCommand(CancelEdit);

        public string? Name
        {
            get => _user.Name;
            set { _user.Name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string? Surname
        {
            get => _user.Surname;
            set { _user.Surname = value; OnPropertyChanged(nameof(Surname)); }
        }

        public int? Age
        {
            get => _user.Age;
            set { _user.Age = value; OnPropertyChanged(nameof(Age)); }
        }

        public string? Job
        {
            get => _user.Job;
            set { _user.Job = value; OnPropertyChanged(nameof(Job)); }
        }

        public UpdateUserViewModel(User user, IUserRepository repository)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        private void SaveUser()
        {
            try
            {
                _repository.UpdateUser(_user);
                MessageBox.Show("User updated successfully.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelEdit()
        {
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
