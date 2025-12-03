using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PersonRecord.Export;
using PersonRecord.Models;
using PersonRecord.Models.Enum;
using PersonRecord.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PersonRecord.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<User> Users { get; set; }

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

        private RelayCommand _editUserDetailsCommand;
        public RelayCommand EditUserDetailsCommand => _editUserDetailsCommand ?? (_editUserDetailsCommand = new RelayCommand(EditUserDetails));

        private RelayCommand _saveUserDetailsCommand;
        public RelayCommand SaveUserDetailsCommand => _saveUserDetailsCommand ?? (_saveUserDetailsCommand = new RelayCommand(SaveUserDetails));
        public Array ExportFormats => Enum.GetValues(typeof(ExportFormat));

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
                    
                   
            }
        }
    }
}
