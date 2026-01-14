using PersonRecord.Models;
using PersonRecord.ViewModel;
using System.Windows;

namespace PersonRecord.Views
{
    public partial class AddUser : Window
    {
        private readonly IUserRepository _userRepository;

        public AddUser(IUserRepository userRepository)
        {
            InitializeComponent();
            _userRepository = userRepository;
            AddUserViewModel addUserViewModel = new AddUserViewModel(_userRepository);
            this.DataContext = addUserViewModel;
        }
    }
}
