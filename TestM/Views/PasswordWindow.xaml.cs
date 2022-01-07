using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private void LogInButton(object sender, RoutedEventArgs e)
        {
            (DataContext as PasswordViewModel).CheckUserPassword(PasswordBox.Password, this);
        }
        private void LogIn(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                LogInButton(sender, e);
            }
        }
    }
}
