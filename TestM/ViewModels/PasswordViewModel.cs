﻿using System.Windows.Media;
using TestM.Command;
using TestM.Data;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class PasswordViewModel : ViewModel
    {
        #region Commands
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow
        {
            get 
            {
                return minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
                {
                    PasswordWindow wnd = obj as PasswordWindow;
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
                    PasswordWindow wnd = obj as PasswordWindow;
                    wnd.Close();
                }));
            }
        }
        #endregion
        public void CheckUserPassword(string password, PasswordWindow passwordWindow)
        {
            ChangePassword passwordOriginal = new ChangePassword();
            if (password == passwordOriginal.Read())
            {
                passwordWindow.Close();
                var questionWindow = new QuestionWindow();
                questionWindow.Show();
            }
            else
            {
                passwordWindow.PasswordBox.Clear();
                passwordWindow.PasswordBox.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
        }
    }
}
