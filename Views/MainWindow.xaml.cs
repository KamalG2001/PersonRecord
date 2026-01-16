using PersonRecord.FileReader;
using PersonRecord.JsonReader;
using PersonRecord.Models;
using PersonRecord.Models.Providers;
using PersonRecord.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace PersonRecord
{


    public partial class MainWindow : Window
    {
        //public MainWindow()
        //{
        //    InitializeComponent();

        //    var storageType = StorageType.Repository;

        //    IUserRepository? repository;
        //    if (storageType == StorageType.Repository)
        //        repository = (JsonUserRepository?)new JsonUserRepository();
        //    else
        //        repository = null;


        //    DataContext = new MainViewModel(
        //        new FileDialogService(),
        //        storageType,
        //        repository
        //    );
        //}
        private IUserRepository _repository;
        public MainWindow()
        {
            InitializeComponent();

            _repository = new JsonUserRepository();
            var users = _repository.GetAllUsers();

            foreach (var user in users)
            {
                UserManager.AddUser(user);
            }
            DataContext = new MainViewModel(new FileDialogService(), _repository);
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}