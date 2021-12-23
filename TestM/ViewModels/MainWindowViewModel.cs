using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.Command;
using TestM.Pages;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Command
        private RelayCommand checkPassword;
        public RelayCommand CheckPassword 
        { 
            get
            {
                return checkPassword ?? (checkPassword = new RelayCommand(obj =>
                {
                    var password = new PasswordWindow();
                    password.ShowDialog();
                }));
            }
            
        }
        private RelayCommand openSetting;
        public RelayCommand OpenSetting
        {
            get
            {
                return openSetting ?? (openSetting = new RelayCommand(obj =>
                {
                    SettingWindow wnd = new SettingWindow();
                    wnd.Show();
                }));
            }
        }
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow
        {
            get
            {
                return minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
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
                    MainWindow wnd = obj as MainWindow;
                    wnd.Close();
                }));
            }
        }
        private RelayCommand startTest;
        public RelayCommand StartTest
        {
            get
            {
                return startTest ?? (startTest = new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
                    wnd.MainFrame.Content = new StartPageTest(wnd);
                }));
            }
        }
        #endregion
    }
}
