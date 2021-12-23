using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.Command;
using TestM.Data;
using TestM.Pages;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    class StartPageTestViewModel : ViewModel
    {
        MainWindow window;
        #region Command
        private RelayCommand startText;
        public RelayCommand StartTest
        {
            get 
            {
                return startText ?? (startText = new RelayCommand(obj =>
                {
                    //AddResultToFile.Write(Name, Subdivision, Date);
                    window.MainFrame.Content = new FirstPageTest(window);
                }));
            }
        }
        #endregion
        #region Property
        private string name;
        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string subdivision;
        public string Subdivision
        {
            get { return subdivision; }
            set
            {
                subdivision = value;
                OnPropertyChanged(nameof(subdivision));
            }
        }
        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged(nameof(date));
            }
        }
        #endregion
        public StartPageTestViewModel(MainWindow window)
        {
            this.window = window;
        }
    }
}
