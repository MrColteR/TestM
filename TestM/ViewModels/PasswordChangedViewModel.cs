using TestM.Command;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class PasswordChangedViewModel : ViewModel
    {
        #region Commands
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow
        {
            get
            {
                return minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
                {
                    PasswordChanged wnd = obj as PasswordChanged;
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
                    PasswordChanged wnd = obj as PasswordChanged;
                    wnd.Close();
                }));
            }
        }
        private RelayCommand okeyCommand;
        public RelayCommand OkeyCommand
        {
            get
            {
                return okeyCommand ?? (okeyCommand = new RelayCommand(obj =>
                {
                    PasswordChanged wnd = obj as PasswordChanged;
                    wnd.Close();
                }));
            }
        }
        #endregion
    }
}
