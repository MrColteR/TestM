using System.Windows;
using System.Windows.Controls;
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
            (DataContext as PasswordViewModel).CheckUserPassword(PasswordBox.Password, this);
        }
    }
}
