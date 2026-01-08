using PersonRecord.FileReader;
using PersonRecord.JsonReader;
using PersonRecord.Models;
using PersonRecord.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PersonRecord
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var mapper = new JsonService();
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonReader", "Users.json");

            if (File.Exists(jsonPath))
            {
                var jsonData = File.ReadAllText(jsonPath);
                var users = mapper.MapFromJson(jsonData);

                foreach (var user in users)
                {
                    UserManager.AddUser(user);
                }
            }
            DataContext = new MainViewModel(new FileDialogService());
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