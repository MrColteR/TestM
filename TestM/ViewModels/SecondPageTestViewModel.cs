using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    public class SecondPageTestViewModel : ViewModel
    {
        MainWindow window;
        int resultPoints;
        public SecondPageTestViewModel(MainWindow window, int resultPoints)
        {
            this.window = window;
            this.resultPoints = resultPoints;
        }
    }
}
