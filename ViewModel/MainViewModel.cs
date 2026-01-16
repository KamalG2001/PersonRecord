using GalaSoft.MvvmLight.Command;
using PersonRecord.Export;
using PersonRecord.FileReader;
using PersonRecord.Models;
using PersonRecord.Models.Enum;
using PersonRecord.Models.Providers;
using PersonRecord.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace PersonRecord.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string? _name;
        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private string? _surname;
        public string? Surname
        {
            get => _surname;
            set { _surname = value; OnPropertyChanged(nameof(Surname)); }
        }

        private int? _age;
        public int? Age
        {
            get => _age;
            set { _age = value; OnPropertyChanged(nameof(Age)); }
        }

        private string? _job;
        public string? Job
        {
            get => _job;
            set { _job = value; OnPropertyChanged(nameof(Job)); }
        }

        public ObservableCollection<User> Users { get; set; }

        private User? _selectedUser;
        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser == value) return;
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        private bool _canDeleteUser = true;
        public bool CanDeleteUser
        {
            get => _canDeleteUser;
            set { _canDeleteUser = value; OnPropertyChanged(nameof(CanDeleteUser)); }
        }

        private bool _canUpdateUser = true;
        public bool CanUpdateUser
        {
            get => _canUpdateUser;
            set { _canUpdateUser = value; OnPropertyChanged(nameof(CanUpdateUser)); }
        }

        private object _currentView = new object();
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }

        private object _updateView = new object();
        public object UpdateView
        {
            get => _updateView;
            set { _updateView = value; OnPropertyChanged(nameof(UpdateView)); }
        }

        private string _fileContent = string.Empty;
        public string FileContent
        {
            get => _fileContent;
            set { _fileContent = value; OnPropertyChanged(nameof(FileContent)); }
        }

        private RelayCommand? _editUserDetailsCommand;
        public RelayCommand EditUserDetailsCommand =>
            _editUserDetailsCommand ??= new RelayCommand(EditUserDetails);

        private RelayCommand? _saveUserDetailsCommand;
        public RelayCommand SaveUserDetailsCommand =>
            _saveUserDetailsCommand ??= new RelayCommand(SaveUserDetails);

        private RelayCommand? _openUserDetailsCommand;
        public RelayCommand OpenUserDetailsCommand =>
            _openUserDetailsCommand ??= new RelayCommand(OpenUserDetails);

        public Array ExportFormats => System.Enum.GetValues(typeof(ExportFormat));

        private RelayCommand? _deleteSelectedUserCommand;
        public RelayCommand DeleteSelectedUserCommand =>
            _deleteSelectedUserCommand ??= new RelayCommand(DeleteSelectedUser, () => CanDeleteUser);

        private RelayCommand? _updateUserCommand;
        public RelayCommand UpdateUserCommand =>
            _updateUserCommand ??= new RelayCommand(UpdateUser, () => CanUpdateUser);

        private readonly IFileDialogService _dialogService;
        private readonly IDataProvider _dataProvider;
        private readonly IExporterFactory _exporterFactory;

        public RelayCommand OpenFileCommand { get; }

        private int _selectedFormatValue = -1;
        public ExportFormat SelectedFormat
        {
            get => _selectedFormatValue >= 0 ? (ExportFormat)_selectedFormatValue : default;
            set { _selectedFormatValue = (int)value; }
        }

        public MainViewModel(IFileDialogService dialogService, StorageType storageType, IUserRepository? repository = null)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _dataProvider = DataProviderFactory.CreateProvider(storageType, repository);
            _exporterFactory = new ExporterFactory();
            
            Users = new ObservableCollection<User>(_dataProvider.GetAllUsers());
            OpenFileCommand = new RelayCommand(OpenFile);
        }

        private void OpenFile()
        {
            var filePath = _dialogService.OpenFile("Json Files|*.json|All Files|*.*");
            if (filePath != null)
            {
                try
                {
                    FileContent = File.ReadAllText(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}", "File Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditUserDetails()
        {
            var addUserView = new AddUser(
                _dataProvider is IUserRepository repo ? repo : null,
                _dataProvider
            )
            {
                DataContext = new AddUserViewModel(_dataProvider)
            };

            addUserView.ShowDialog();
            RefreshUsers();
        }

        private void OpenUserDetails()
        {
            ShowWindow(new AddUser(
                _dataProvider is IUserRepository repo ? repo : null,
                _dataProvider
            ));
        }

        private void ShowWindow(Window window)
        {
            if (window is Window win)
            {
                win.Show();
            }
        }

        private void SaveUserDetails()
        {
            if (_selectedFormatValue < 0)
            {
                MessageBox.Show("Please select an export format.", "Export Format Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var exporter = _exporterFactory.CreateExporter(SelectedFormat);
                var fileName = GenerateFileName();
                var filter = GenerateFilter();
                var filePath = _dialogService.SaveFile(filter, fileName);

                if (filePath != null)
                {
                    exporter.Export(Users.ToList(), filePath);
                    MessageBox.Show($"Data exported successfully to:\n{filePath}", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during export: {ex.Message}", "Export Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteSelectedUser()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _dataProvider.DeleteUser(SelectedUser);
                Users.Remove(SelectedUser);
                MessageBox.Show("User deleted successfully.", "Delete Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateUser()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user to update.", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Fix: Pass IUserRepository to UpdateUserViewModel, not IDataProvider
            IUserRepository? repository = _dataProvider as IUserRepository;
            if (repository == null)
            {
                MessageBox.Show("Update operation requires a repository.", "Repository Missing", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var updateUserView = new UpdateUser
            {
                DataContext = new UpdateUserViewModel(SelectedUser, repository)
            };
            updateUserView.ShowDialog();

            try
            {
                _dataProvider.UpdateUser(SelectedUser);
                RefreshUsers();
                MessageBox.Show("User updated successfully.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshUsers()
        {
            Users.Clear();
            foreach (var user in _dataProvider.GetAllUsers())
            {
                Users.Add(user);
            }
        }

        private string GenerateFileName() =>
            $"Users.{SelectedFormat.ToString().ToLower()}".Replace(" ", "_");

        private string GenerateFilter() =>
            $"{SelectedFormat} files|*.{SelectedFormat.ToString().ToLower()}";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

