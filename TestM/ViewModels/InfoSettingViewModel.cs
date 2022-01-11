using System;
using System.Collections.Generic;
using System.IO;
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

        private Dictionary<int, bool> buttonsStates;
        private int countType;
        private bool checkStyleButton;

        #region Commands
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow => closeWindow ?? (closeWindow = new RelayCommand(obj =>
        {
            InfoSettingWindow wnd = obj as InfoSettingWindow;
            wnd.Close();
        }));

        private RelayCommand openComboStyleApp;
        public RelayCommand OpenComboStyleApp => openComboStyleApp ?? (openComboStyleApp = new RelayCommand(obj =>
        {
            InfoSettingWindow wnd = obj as InfoSettingWindow;
            if (!checkStyleButton)
            {
                wnd.GridComboBoxStyle.Visibility = System.Windows.Visibility.Visible;
                checkStyleButton = true;
            }
            else if (checkStyleButton)
            {
                wnd.GridComboBoxStyle.Visibility = System.Windows.Visibility.Hidden;
                checkStyleButton = false;
            }
        }));

        private RelayCommand choiceStyle;
        public RelayCommand ChoiceStyle => choiceStyle ?? (choiceStyle = new RelayCommand(obj =>
        {
            InfoSettingWindow wnd = obj as InfoSettingWindow;
            var app = (App)Application.Current;

            wnd.GridComboBoxStyle.Visibility = Visibility.Hidden;
            if (wnd.Default.IsFocused)
            {
                wnd.TypeButton.Content = wnd.Default.Content;
                StyleApp = wnd.Default.Content.ToString();
                app.ChangeTheme(new Uri("/Styles/DefaultStyle.xaml", UriKind.RelativeOrAbsolute));
                service.SaveStyleApp(fileInfo, StyleApp);
            }
            if (wnd.Dark.IsFocused)
            {
                wnd.TypeButton.Content = wnd.Dark.Content;
                StyleApp = wnd.Dark.Content.ToString();
                app.ChangeTheme(new Uri("/Styles/DarkStyle.xaml", UriKind.RelativeOrAbsolute));
                service.SaveStyleApp(fileInfo, StyleApp);
            }
            checkStyleButton = false;
        }));

        private RelayCommand changeSettingInfo;
        public RelayCommand ChangeSettingInfo => changeSettingInfo ?? (changeSettingInfo = new RelayCommand(obj =>
        {
            service.SaveQuestionsInfo(fileInfo, countQuestionOneType, countQuestion);
            service.SaveButtonsStates(fileInfo, buttonsStates, minimalCountPoints);
            InfoSettingWindow wnd = obj as InfoSettingWindow;
            wnd.Close();
        }));
        #endregion
        #region Property
        private string styleApp;
        public string StyleApp
        {
            get => styleApp;
            set 
            {
                styleApp = value;
                OnPropertyChanged(nameof(StyleApp));
            }
        }
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
            checkStyleButton = false;

            StyleApp = service.OpenStyleApp(fileInfo);
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
