using System.IO;
using System.Windows.Media;
using TestM.Command;
using TestM.Data;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class PasswordViewModel : ViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";
        #region Commands
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow => closeWindow ?? (closeWindow = new RelayCommand(obj =>
        {
            PasswordWindow wnd = obj as PasswordWindow;
            wnd.Close();
        }));
        #endregion
        private bool _isFocused = false;

        public bool IsTextBoxFocused
        {
            get { return _isFocused; }
            set
            {
                if (_isFocused == value)
                {
                    _isFocused = false;

                    
                    OnPropertyChanged("IsTextBoxFocused");
                }
                _isFocused = value;
                OnPropertyChanged("IsTextBoxFocused");
            }



        }
        public void CheckUserPassword(string password, PasswordWindow passwordWindow)
        {
            JsonFileService service = new JsonFileService();
            if (password == service.OpenPassword(fileInfo))
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
