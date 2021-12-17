using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestM.ViewModels;

namespace TestM.Views
{
    /// <summary>
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public PasswordWindow()
        {
            InitializeComponent();
            DataContext = new PasswordViewModel();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as PasswordViewModel).CheckUserPassword(PasswordBox.Password))
            {
                Close();
            }
            else
            {
                PasswordBox.Clear();
                PasswordBox.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
        }
    }
}
