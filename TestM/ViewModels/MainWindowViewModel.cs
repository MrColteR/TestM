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

        #endregion
    }
}
