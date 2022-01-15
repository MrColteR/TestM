using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.Command;
using TestM.Views;

namespace TestM.ViewModels
{
    public class EmptyFieldsViewModel
    {
        #region Commands
        private RelayCommand okeyCommand;
        public RelayCommand OkeyCommand => okeyCommand ?? (okeyCommand = new RelayCommand(obj =>
        {
            EmptyFieldsWindow wnd = obj as EmptyFieldsWindow;
            wnd.Close();
        }));
        #endregion
    }
}
