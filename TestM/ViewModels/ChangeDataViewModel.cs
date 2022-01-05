﻿using System;
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
    public class ChangeDataViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        public readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";

        JsonFileService service;
             
        #region Commands
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow => closeWindow ?? (closeWindow = new RelayCommand(obj =>
        {
            ChangeData wnd = obj as ChangeData;
            wnd.Close();
        }));
        private RelayCommand saveChange;
        public RelayCommand SaveChange => saveChange ?? (saveChange = new RelayCommand(obj =>
        {
            service.SaveFileCheck(fileInfo, true);

            ChangeData wnd = obj as ChangeData;
            wnd.Close();
        }));
        private RelayCommand notSaveChange;
        public RelayCommand NotSaveChange => notSaveChange ?? (notSaveChange = new RelayCommand(obj =>
        {
            service.SaveFileCheck(fileInfo, false);

            ChangeData wnd = obj as ChangeData;
            wnd.Close();
        }));
        #endregion
        public ChangeDataViewModel()
        {
            service = new JsonFileService();
        }
    }
}
