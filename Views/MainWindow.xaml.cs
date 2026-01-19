using PersonRecord.FileReader;
using PersonRecord.Models;
using PersonRecord.ViewModel;
using System.Windows;

namespace PersonRecord
{
    public partial class MainWindow : Window
    {
        private IUserRepository _repository;

        public MainWindow()
        {
            InitializeComponent();
            _repository = new JsonUserRepository();   ///<-- Use json repository
            //_repository = new InMemoryUserRepository(); ///<-- Use in-memory repository
            var users = _repository.GetAllUsers();

            foreach (var user in users)
            {
                UserManager.AddUser(user);
            }
            DataContext = new MainViewModel(new FileDialogService(), _repository);
        }
    }
}