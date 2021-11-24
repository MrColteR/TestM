using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestM.Command;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class PasswordViewModel : ViewModel
    {
        //private int checkPas = 0;
        #region Command
        private RelayCommand checkUserPassword;
        public RelayCommand CheckUserPassword
        { 
            get
            {
                return checkUserPassword ?? (checkUserPassword = new RelayCommand(obj =>
                {
                    var passwordBox = obj as PasswordBox;
                    if (passwordBox.Password == "Пароль")
                    {
                        var questionWindow = new QuestionWindow();
                        //checkPas = 1;
                        questionWindow.ShowDialog();
                    }
                    else
                    {
                        //checkPas = 2;
                    }
                }));
            }
        }
        #endregion
        //public string PasswordEdit()
        //{
        //    if (checkPas == 1)
        //    {
        //        return "okey";
        //    }
        //    if (checkPas == 2)
        //    {
        //        return "error";
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
    }
}
