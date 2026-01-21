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

            //DI Container (Ninject, Autofac)
            _repository = new JsonUserRepository(); ///<< Use JSON
            //_repository = new InMemoryUserRepository(); ///<< Use Memory

            var users = _repository.GetAllUsers();

            foreach (var user in users)
            {
                UserManager.AddUser(user);
            }
            DataContext = new MainViewModel(new FileDialogService(), _repository);
        }


        
    }
}