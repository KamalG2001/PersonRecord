using Autofac;
using PersonRecord.Repos;
using PersonRecord.Services;
using PersonRecord.ViewModel;
using System.Windows;

namespace PersonRecord
{
    public partial class MainWindow : Window
    {
        private readonly IContainer _container;

        public MainWindow()
        {
            InitializeComponent();

            // DI container Autofac setup
            _container = ContainerSetup.BuildContainer();  // Uses default configuration

            // Resolve services from container
            var userService = _container.Resolve<IUserService>();
            var repository = _container.Resolve<IUserRepository>();
            var dialogService = _container.Resolve<IFileDialogService>();

            DataContext = new MainViewModel(dialogService, repository, userService);
        }
    }
}