using System.Windows;
using System.Windows.Input;
using TestM.ViewModels;

namespace TestM.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }
        private void ChangePasswordButton(object sender, RoutedEventArgs e)
        {
            (DataContext as SettingWindowViewModel).CheckUserPassword(PasswordBoxOld.Password, PasswordBoxNew.Password, this);
        }
        private void ChangePassword(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                ChangePasswordButton(sender, e);
            }
        }
    }
}
