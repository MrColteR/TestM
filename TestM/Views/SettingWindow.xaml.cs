using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            (DataContext as SettingWindowViewModel).CheckUserPassword(PasswordBoxOld.Password, PasswordBoxNew.Password, this);
        }
    }
}
