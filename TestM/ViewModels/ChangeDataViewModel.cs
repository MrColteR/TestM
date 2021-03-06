using System.IO;
using TestM.Command;
using TestM.Data;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class ChangeDataViewModel : ViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        public readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";

        private readonly JsonFileService service;
     
        #region Commands
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow => closeWindow ?? (closeWindow = new RelayCommand(obj =>
        {
            ChangeDataWindow wnd = obj as ChangeDataWindow;
            wnd.Close();
        }));
        private RelayCommand saveChange;
        public RelayCommand SaveChange => saveChange ?? (saveChange = new RelayCommand(obj =>
        {
            service.SaveFileCheck(fileInfo, true);

            ChangeDataWindow wnd = obj as ChangeDataWindow;
            wnd.Close();
        }));
        private RelayCommand notSaveChange;
        public RelayCommand NotSaveChange => notSaveChange ?? (notSaveChange = new RelayCommand(obj =>
        {
            service.SaveFileCheck(fileInfo, false);

            ChangeDataWindow wnd = obj as ChangeDataWindow;
            wnd.Close();
        }));
        #endregion
        public ChangeDataViewModel()
        {
            service = new JsonFileService();
        }
    }
}
