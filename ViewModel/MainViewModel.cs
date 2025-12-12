using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PersonRecord.Export;
using PersonRecord.FileReader;
using PersonRecord.Models;
using PersonRecord.Models.Enum;
using PersonRecord.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace PersonRecord.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public string? Job { get; set; }
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
        public bool CanDeleteUser { get; set; } = true;
        public bool CanUpdateUser { get; set; } = true;
        public MainViewModel()
        {
            Users = UserManager.GetUsers();
        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
            }
        }
        private object _updateView;
        public object UpdateView
        {
            get => _updateView;
            set
            {
                _updateView = value;
                //OnPropertyChanged(nameof(UpdateView));
            }
        }
        private string _fileContent;
        public string FileContent
        {
            get => _fileContent;
            set { _fileContent = value; OnPropertyChanged(nameof(FileContent)); }
        }
        private RelayCommand _editUserDetailsCommand;
        public RelayCommand EditUserDetailsCommand => _editUserDetailsCommand ?? (_editUserDetailsCommand = new RelayCommand(EditUserDetails));

        private RelayCommand _saveUserDetailsCommand;
        public RelayCommand SaveUserDetailsCommand => _saveUserDetailsCommand ?? (_saveUserDetailsCommand = new RelayCommand(SaveUserDetails));
        private RelayCommand _openUserDetailsCommand;
        public RelayCommand OpenUserDetailsCommand => _openUserDetailsCommand ?? (_openUserDetailsCommand = new RelayCommand(OpenUserDetails));
        public Array ExportFormats => Enum.GetValues(typeof(ExportFormat));
        private RelayCommand _deleteSelectedUserCommand;
        public RelayCommand DeleteSelectedUserCommand => _deleteSelectedUserCommand ?? (_deleteSelectedUserCommand = new RelayCommand(DeleteSelectedUser, () => CanDeleteUser));
        private RelayCommand _updateUserCommand;
        public RelayCommand UpdateUserCommand => _updateUserCommand ?? (_updateUserCommand = new RelayCommand(UpdateUser, () => CanUpdateUser));

        private readonly IFileDialogService _dialogService;
        public MainViewModel(IFileDialogService dialogService)
        {
            _dialogService = dialogService;
            OpenFileCommand = new RelayCommand(OpenFile);
        }

        

        public RelayCommand OpenFileCommand { get; }
       
        private void OpenFile()
        {
            var filePath = _dialogService.OpenFile("Json Files|*.json|All Files|*.*");

            if (filePath != null)
            {
                FileContent = File.ReadAllText(filePath);
            }
        }
        private ExportFormat _selectedFormat;
        public ExportFormat SelectedFormat
        {
            get => _selectedFormat;
            set
            {
                _selectedFormat = value;
            }
        }
        private void EditUserDetails()
        {
            CurrentView = new AddUser();
            var a = (Window)CurrentView;
            a.Show();
        }

        private void SaveUserDetails()
        {
            IExport exporter;
            switch (SelectedFormat)
            {
                case ExportFormat.Txt:
                    exporter = new TxtExport();
                    break;
                case ExportFormat.Json:
                    exporter = new JsonExport();
                    break;
                case ExportFormat.Csv:
                    exporter = new CsvExporter();
                    break; 
                default:
                    return;
            }
            var service = new ExportService(exporter);

            var dialog = new SaveFileDialog();

            dialog.Filter = $"{SelectedFormat} files|*.{SelectedFormat.ToString().ToLower()}";
            dialog.FileName = $"Users.{SelectedFormat.ToString().ToLower()}";

            if (dialog.ShowDialog() == true)
            {
                service.Export(Users.ToList(), dialog.FileName);
                MessageBox.Show($"Data exported successfully to:\n{dialog.FileName}");
            }
        }
        private void OpenUserDetails()
        {
            CurrentView = new AddUser();
            var a = (Window)CurrentView;
            a.Show();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
       
        private void DeleteSelectedUser()
        {
            UserManager.DeleteSelectedUser(SelectedUser);
        }

        private void UpdateUser()
        {
            if (SelectedUser == null)
                return;
            var updateUserView = new UpdateUser
            {
                DataContext = new UpdateUserViewModel(SelectedUser)
            };
            updateUserView.ShowDialog();
        }

    }
}

