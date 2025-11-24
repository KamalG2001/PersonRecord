using PersonRecord.Commands;
using PersonRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonRecord.ViewModel
{
    public class AddUserViewModel
    {

        public ICommand AddUserCommand { get; set; }

        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public string? Job { get; set; }

        public AddUserViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
        }

        private bool CanAddUser(object obj)
        {
            return true;
        }

        private void AddUser(object obj)
        {

            UserManager.AddUser(new User() { Name = Name, Surname = Surname, Age = Age,Job = Job});

        }
    }
}
