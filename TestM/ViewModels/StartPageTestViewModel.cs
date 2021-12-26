using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.Pages;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    class StartPageTestViewModel : ViewModel
    {
        //private static string path = Directory.GetCurrentDirectory();
        //private readonly string fileResult = path.Substring(0, path.IndexOf("bin")) + "Result.json";
        //private readonly string fileWindow = path.Substring(0, path.IndexOf("bin")) + "Window.json";
        //#region Command
        //private RelayCommand startText;
        //public RelayCommand StartTest
        //{
        //    get 
        //    {
        //        return startText ?? (startText = new RelayCommand(obj =>
        //        {
        //            //Frame windoww = obj as Frame;
        //            //windoww.Content = new FirstPageTest();
        //            //AddResultToFile.Write(Name, Subdivision, Date);
        //            //window[0].MainFrame.Content = new FirstPageTest();
        //        }));
        //    }
        //}
        //#endregion
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
        public StartPageTestViewModel()
        {
            
        }
    }
}
