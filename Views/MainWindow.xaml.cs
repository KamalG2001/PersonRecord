using PersonRecord.FileReader;
using PersonRecord.JsonReader;
using PersonRecord.Models;
using PersonRecord.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace PersonRecord
{
    public partial class MainWindow : Window
    {
        private IUserRepository _repository;

        public MainWindow()
        {
            InitializeComponent();

            _repository = new JsonUserRepository(); ///<< Use JSON
            //_repository = new InMemoryUserRepository(); ///<< Use Memory

            var users = _repository.GetAllUsers();

            foreach (var user in users)
            {
                UserManager.AddUser(user);
            }
            DataContext = new MainViewModel(new FileDialogService(), _repository);
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserList.Items.Filter = FilterMethod;
        }

        private bool FilterMethod(object obj)
        {
            var user = (User)obj;
            return user.Name.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}