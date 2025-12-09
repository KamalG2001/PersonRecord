using GalaSoft.MvvmLight.Command;
using PersonRecord.Views;
using System.Windows;

namespace PersonRecord.ViewModel
{
    public class UpdateUserViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public string? Job { get; set; }

        public bool CanUpdateUser { get; set; } = true;

        private RelayCommand _updateUserCommand;
        public RelayCommand UpdateUserCommand => _updateUserCommand ?? (_updateUserCommand = new RelayCommand(UpdateSelectedUser, () => CanUpdateUser));

        private void UpdateSelectedUser()
        {
           
        }
    }
}
