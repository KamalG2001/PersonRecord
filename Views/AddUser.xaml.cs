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
            //_userRepository = userRepository;
            //EditUserViewModel addUserViewModel = (_userRepository);
            //this.DataContext = new EditUserViewModel(null);
        }
    }
}
