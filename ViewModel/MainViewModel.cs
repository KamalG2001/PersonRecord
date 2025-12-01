using PersonRecord.Models;
using PersonRecord.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace PersonRecord.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        //public ICommand ShowWindowCommand { get; set; }
        public MainViewModel()
        {
            Users = UserManager.GetUsers();
            //ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
        }
        private bool CanShowWindow(object obj)
        {
            return true;
        }
        private void ShowWindow(object obj)
        {
            var mainWindow = obj as Window;

            AddUser addUserWin = new AddUser();
            addUserWin.Owner = mainWindow;
            addUserWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addUserWin.Show();
        }

        public ICommand OpenWindowCommand { get; }

        private RelayCommand _editUserDetailsCommand;
        public RelayCommand EditUserDetailsCommand => _editUserDetailsCommand ?? (_editUserDetailsCommand = new RelayCommand(EditUserDetails));
        public event Action RequestOpenUserDetails;

        private void EditUserDetails()
        {
            RequestOpenUserDetails?.Invoke();   // notify the View
        }

    }
}
