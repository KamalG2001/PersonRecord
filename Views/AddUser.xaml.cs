using PersonRecord.Models;
using PersonRecord.Models.Providers;
using PersonRecord.ViewModel;
using System.Windows;

namespace PersonRecord.Views
{
    public partial class AddUser : Window
    {
        private readonly IUserRepository _userRepository;

        public AddUser(IUserRepository userRepository, IDataProvider dataProvider)
        {
            InitializeComponent();
            _userRepository = userRepository;
            AddUserViewModel addUserViewModel = new AddUserViewModel(dataProvider);
            this.DataContext = addUserViewModel;
        }
    }
}
