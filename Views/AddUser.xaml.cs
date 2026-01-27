using PersonRecord.Repos;
using PersonRecord.Services;
using PersonRecord.ViewModel;
using System.Windows;

namespace PersonRecord.Views
{
    public partial class AddUser : Window
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public AddUser(IUserRepository userRepository, IUserService userService)
        {
            InitializeComponent();
        }
    }
}
