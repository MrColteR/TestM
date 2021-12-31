﻿using System.IO;
using System.Windows.Media;
using TestM.Command;
using TestM.Data;
using TestM.Views;

namespace TestM.ViewModels
{
    public class SettingWindowViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        private readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";
        #region Commands
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow
        {
            get 
            {
                return minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
                {
                    SettingWindow wnd = obj as SettingWindow;
                    wnd.WindowState = System.Windows.WindowState.Minimized;
                }));
            }
        }
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow ?? (closeWindow = new RelayCommand(obj =>
                {
                    SettingWindow wnd = obj as SettingWindow;
                    wnd.Close();
                }));
            }
        }
        #endregion
        public void CheckUserPassword(string oldPassword, string newPassword, SettingWindow settingWindow)
        {
            JsonFileService service = new JsonFileService();
            if (oldPassword == service.OpenPassword(fileInfo))
            {
                service.SavePassword(fileInfo, newPassword);
                settingWindow.Close();
                PasswordChanged passwordChangedWindow = new PasswordChanged();
                passwordChangedWindow.Show();
            }
            else
            {
                settingWindow.PasswordBoxOld.Clear();
                settingWindow.PasswordBoxNew.Clear();
                settingWindow.PasswordBoxOld.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
                settingWindow.PasswordBoxNew.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
        }
    }
}
