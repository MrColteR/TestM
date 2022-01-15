using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TestM.Command;
using TestM.Data;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class InfoSettingViewModel : ViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        public readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";

        private readonly JsonFileService service;
        private readonly ConvertToString converter;

        private Dictionary<int, bool> buttonsStates;
        private int countType;

        #region Commands
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow => closeWindow ?? (closeWindow = new RelayCommand(obj =>
        {
            InfoSettingWindow wnd = obj as InfoSettingWindow;
            wnd.Close();
        }));
        
        private RelayCommand changeSettingInfo;
        public RelayCommand ChangeSettingInfo => changeSettingInfo ?? (changeSettingInfo = new RelayCommand(obj =>
        {
            var app = (App)Application.Current;

            if (ComboBoxStyleText == StyleEnum.darkStyle)
            {
                app.ChangeTheme(new Uri("/Styles/DarkStyle.xaml", UriKind.RelativeOrAbsolute));
                service.SaveStyleApp(fileInfo, (string)converter.Convert(ComboBoxStyleText, null, null, null));
            }
            else
            {
                app.ChangeTheme(new Uri("/Styles/DefaultStyle.xaml", UriKind.RelativeOrAbsolute));
                service.SaveStyleApp(fileInfo, (string)converter.Convert(ComboBoxStyleText, null, null, null));
            }

            service.SaveQuestionsInfo(fileInfo, countQuestionOneType, countQuestion);
            service.SaveButtonsStates(fileInfo, buttonsStates, minimalCountPoints);

            InfoSettingWindow wnd = obj as InfoSettingWindow;
            wnd.Close();
        }));
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
        private StyleEnum comboBoxStyleText;
        public StyleEnum ComboBoxStyleText
        {
            get => comboBoxStyleText; 
            set => comboBoxStyleText = value; 
        }

        #endregion
        public InfoSettingViewModel()
        {
            service = new JsonFileService();
            converter = new ConvertToString();

            CountQuestionOneType = service.OpenCountQuestionOneType(fileInfo);
            CountQuestion = service.OpenCountQuestion(fileInfo);
            MinimalCountPoints = service.OpenMinimalCountPonits(fileInfo);
            buttonsStates = service.OpenButtonStates(fileInfo);
            comboBoxStyleText = (StyleEnum)converter.ConvertBack(service.OpenStyleApp(fileInfo), null, null, null);
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
