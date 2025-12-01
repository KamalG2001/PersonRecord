using PersonRecord.Models;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace PersonRecord.ViewModel
{
    public class AddUserViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public string? Job { get; set; }

        public bool CanAddUser { get; set; } = true;

        private RelayCommand _addUserCommand;
        public RelayCommand AddUserCommand => _addUserCommand ?? (_addUserCommand = new RelayCommand(AddUser, () => CanAddUser));

        private void AddUser()
        {
            UserManager.AddUser(new User() { Name = Name, Surname = Surname, Age = Age,Job = Job});
        }
    }
}
