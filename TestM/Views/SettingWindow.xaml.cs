using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            DataContext = new SettingWindowViewModel();
        }

        private void btn_ChangePassword(object sender, RoutedEventArgs e)
        {
            if ((DataContext as SettingWindowViewModel).CheckUserPassword(PasswordBoxOld.Password, PasswordBoxNew.Password))
            {
                Close();
                MessageBox.Show("Пароль изменен", "Сообщение", MessageBoxButton.OK);
            }
            else
            {
                PasswordBoxOld.Clear();
                PasswordBoxNew.Clear();
                MessageBox.Show("Пароль неверный", "Сообщение", MessageBoxButton.OK);
            }
        }
    }
}
