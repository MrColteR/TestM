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
            var checkPassword = (DataContext as PasswordViewModel).CheckUserPassword(PasswordBox.Password);
            if (checkPassword)
            {
                Close();
            }
            else
            {
                PasswordBox.Clear();
                MessageBox.Show("Пароль неверный", "Сообщение", MessageBoxButton.OK);
            }
        }
    }
}
