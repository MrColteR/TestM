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
    public class InfoSettingViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        public readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";

        private JsonFileService service;

        private Dictionary<int, bool> buttonsStates;
        private int countType;

        #region Commands
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow ?? (closeWindow = new RelayCommand(obj =>
                {
                    InfoSettingWindow wnd = obj as InfoSettingWindow;
                    wnd.Close();
                }));
            }
        }
        private RelayCommand changeSettingInfo;
        public RelayCommand ChangeSettingInfo
        {
            get
            {
                return changeSettingInfo ?? (changeSettingInfo = new RelayCommand(obj =>
                {
                    service.SaveQuestionsInfo(fileInfo, countQuestionOneType, countQuestion);
                    service.SaveButtonsStates(fileInfo, buttonsStates, minimalCountPoints);
                    InfoSettingWindow wnd = obj as InfoSettingWindow;
                    wnd.Close();
                }));
            }
        }
        #endregion
        #region Property
        private int countQuestionOneType;
        public int CountQuestionOneType
        {
            get => countQuestionOneType;
            set
            {
                countQuestionOneType = value;
                CountQuestion = countType * CountQuestionOneType;
                OnPropertyChanged(nameof(CountQuestionOneType));
            }
        }
        private int countQuestion;
        public int CountQuestion
        {
            get => countQuestion;
            set
            {
                countQuestion = value;
                OnPropertyChanged(nameof(CountQuestion));
            }
        }
        private string minimalCountPoints;
        public string MinimalCountPoints
        {
            get => minimalCountPoints; 
            set
            {
                minimalCountPoints = value;
                OnPropertyChanged(nameof(MinimalCountPoints));
            }
        }
        public bool FirstCheckBoxButton
        {
            get => buttonsStates[1];
            set
            {
                buttonsStates[1] = value;
                CountType();
                CountQuestion = countType * CountQuestionOneType;
                OnPropertyChanged(nameof(FirstCheckBoxButton));
            }
        }
        public bool SecondCheckBoxButton
        {
            get => buttonsStates[2];
            set
            {
                buttonsStates[2] = value;
                CountType();
                CountQuestion = countType * CountQuestionOneType;
                OnPropertyChanged(nameof(SecondCheckBoxButton));
            }
        }
        public bool ThreeCheckBoxButton
        {
            get => buttonsStates[3];
            set
            {
                buttonsStates[3] = value;
                CountType();
                CountQuestion = countType * CountQuestionOneType;
                OnPropertyChanged(nameof(ThreeCheckBoxButton));
            }
        }
        public bool FourtCheckBoxButton
        {
            get => buttonsStates[4];
            set
            {
                buttonsStates[4] = value;
                CountType();
                CountQuestion = countType * CountQuestionOneType;
                OnPropertyChanged(nameof(FourtCheckBoxButton));
            }
        }
        public bool FiveCheckBoxButton
        {
            get => buttonsStates[5];
            set
            {
                buttonsStates[5] = value;
                CountType();
                CountQuestion = countType * CountQuestionOneType;
                OnPropertyChanged(nameof(FiveCheckBoxButton));
            }
        }
        #endregion
        public InfoSettingViewModel()
        {
            service = new JsonFileService();

            CountQuestionOneType = service.OpenCountQuestionOneType(fileInfo);
            CountQuestion = service.OpenCountQuestion(fileInfo);
            MinimalCountPoints = service.OpenMinimalCountPonits(fileInfo);
            buttonsStates = service.OpenButtonStates(fileInfo);
            CountType();
        }
        private void CountType()
        {
            countType = 0;
            foreach (var item in buttonsStates)
            {
                if (item.Value)
                {
                    countType++;
                }
            }
        }
    }
}
