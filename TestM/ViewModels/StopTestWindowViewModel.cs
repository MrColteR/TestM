using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.Command;
using TestM.Data;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class StopTestWindowViewModel : ViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";

        private readonly JsonFileService service;

        #region Commands
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow => closeWindow ?? (closeWindow = new RelayCommand(obj =>
        {
            StopTestWindow wnd = obj as StopTestWindow;
            wnd.Close();
        }));
        private RelayCommand saveChange;
        public RelayCommand SaveChange => saveChange ?? (saveChange = new RelayCommand(obj =>
        {
            service.SaveStopTestCheck(fileInfo, true);

            StopTestWindow wnd = obj as StopTestWindow;
            wnd.Close();
        }));
        private RelayCommand notSaveChange;
        public RelayCommand NotSaveChange => notSaveChange ?? (notSaveChange = new RelayCommand(obj =>
        {
            service.SaveStopTestCheck(fileInfo, false);

            StopTestWindow wnd = obj as StopTestWindow;
            wnd.Close();
        }));
        #endregion

        public StopTestWindowViewModel()
        {
            service = new JsonFileService();
        }
    }
}
