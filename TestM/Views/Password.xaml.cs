using System.Windows;
using System.Windows.Controls;
using TestM.ViewModels;

namespace TestM.Views
{
    /// <summary>
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
            DataContext = new PasswordViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
