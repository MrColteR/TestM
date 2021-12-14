using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.Data;

namespace TestM.ViewModels
{
    public class SettingWindowViewModel
    {
        public bool CheckUserPassword(string oldPassword, string newPassword)
        {
            bool checkPassword;
            ChangePassword passwordOriginal = new ChangePassword();
            if (oldPassword == passwordOriginal.Read())
            {
                passwordOriginal.Change(newPassword);
                checkPassword = true;
            }
            else
            {
                checkPassword = false;
            }

            return checkPassword;
        }
    }
}
