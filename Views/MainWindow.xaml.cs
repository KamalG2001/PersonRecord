using PersonRecord.FileReader;
using PersonRecord.Models;
using PersonRecord.ViewModel;
using System.Windows;
using System.Windows.Controls;

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
            this.DataContext = new MainViewModel();
            //DataContext = new MainViewModel(new FileDialogService());

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