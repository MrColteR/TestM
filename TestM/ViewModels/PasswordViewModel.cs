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
        public bool CheckUserPassword(string password)
        {
            bool checkPassword;
            ChangePassword passwordOriginal = new ChangePassword();
            if (password == passwordOriginal.Read())
            {
                checkPassword = true;
                var questionWindow = new QuestionWindow();
                questionWindow.Show();
            }
            else
            {
                checkPassword = false;
            }

            return checkPassword;
        }
    }
}
