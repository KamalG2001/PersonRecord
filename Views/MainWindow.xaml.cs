using Microsoft.Extensions.DependencyInjection;
using PersonRecord.FileReader;
using PersonRecord.JsonReader;
using PersonRecord.Models;
using PersonRecord.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static PersonRecord.JsonReader.IJsonService;

namespace PersonRecord
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = new MainViewModel();
            DataContext = new MainViewModel(new FileDialogService());
            var services = new ServiceCollection();
            services.AddMapperService();

            var provider = services.BuildServiceProvider();
            var mapper = provider.GetRequiredService<IMapperService>();

            var jsonPath = "Users.json";
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine("JSON file not found!");
                return;
            }

            var jsonData = File.ReadAllText(jsonPath);
            var users = mapper.MapFromJson(jsonData);

            foreach (var user in users)
            {
                UserList.Items.Add(user);
            }

            Console.ReadLine();
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