using GalaSoft.MvvmLight.Command;
using PersonRecord.Models;
using PersonRecord.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
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

        private void EditUserDetails()
        {
            CurrentView = new AddUser();
            var a = (Window)CurrentView;
            a.Show();
        }
    }
}
