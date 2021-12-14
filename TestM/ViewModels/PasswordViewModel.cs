using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestM.Command;
using TestM.Data;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class PasswordViewModel : ViewModel
    {
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
