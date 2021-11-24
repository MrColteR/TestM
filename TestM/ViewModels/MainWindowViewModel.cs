using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.Command;
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
                    var password = new Password();
                    password.ShowDialog();
                }));
            }
            
        }
        #endregion
    }
}
