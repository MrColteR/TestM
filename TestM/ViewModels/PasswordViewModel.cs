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
        #region Property
        private bool checkPas;
        public bool CheckPas
        {
            get
            {
                return checkPas;
            }
        }
        #endregion
        #region Commands
        #endregion
        public bool CheckUserPassword(string password)
        {
            if (password == "Пароль")
            {
                checkPas = true;
                var questionWindow = new QuestionWindow();
                questionWindow.ShowDialog();
            }
            else
            {
                checkPas = false;
            }

            return checkPas;
        }
    }
}
