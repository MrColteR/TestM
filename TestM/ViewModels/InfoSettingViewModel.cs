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

        JsonFileService service;

        private bool checkTypeButton = false;

        #region Commands
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow
        {
            get
            {
                return minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
                {
                    InfoSettingWindow wnd = obj as InfoSettingWindow;
                    wnd.WindowState = System.Windows.WindowState.Minimized;
                }));
            }
        }
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
        private RelayCommand openComboBoxType;
        public RelayCommand OpenComboBoxType
        {
            get
            {
                return openComboBoxType ?? (openComboBoxType = new RelayCommand(obj =>
                {
                    InfoSettingWindow wnd = obj as InfoSettingWindow;
                    if (!checkTypeButton)
                    {
                        wnd.GridComboBoxType.Visibility = System.Windows.Visibility.Visible;
                        checkTypeButton = true;
                    }
                    else if (checkTypeButton)
                    {
                        wnd.GridComboBoxType.Visibility = System.Windows.Visibility.Hidden;
                        checkTypeButton = false;
                    }
                }));
            }
        }
        private RelayCommand choiceType;
        public RelayCommand ChoiceType
        {
            get
            {
                return choiceType ?? (choiceType = new RelayCommand(obj =>
                {
                    InfoSettingWindow wnd = obj as InfoSettingWindow;
                    wnd.GridComboBoxType.Visibility = System.Windows.Visibility.Hidden;
                    if (wnd.OneButton.IsFocused)
                    {
                        wnd.TypeButton.Text = wnd.OneTextBlock.Text;
                        QuestionCollection = Convert.ToInt32(wnd.OneTextBlock.Text);
                    }
                    if (wnd.TwoButton.IsFocused)
                    {
                        wnd.TypeButton.Text = wnd.TwoTextBlock.Text;
                        QuestionCollection = Convert.ToInt32(wnd.TwoTextBlock.Text);
                    }
                    if (wnd.ThreeButton.IsFocused)
                    {
                        wnd.TypeButton.Text = wnd.ThreeTextBlock.Text;
                        QuestionCollection = Convert.ToInt32(wnd.ThreeTextBlock.Text);
                    }
                    if (wnd.FourButton.IsFocused)
                    {
                        wnd.TypeButton.Text = wnd.FourTextBlock.Text;
                        QuestionCollection = Convert.ToInt32(wnd.FourTextBlock.Text);
                    }
                    if (wnd.FiveButton.IsFocused)
                    {
                        wnd.TypeButton.Text = wnd.FiveTextBlock.Text;
                        QuestionCollection = Convert.ToInt32(wnd.FiveTextBlock.Text);
                    }
                    checkTypeButton = false;
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
                    service.SaveQuestionsInfo(fileInfo, questionCollection, countQuestionOneType, countQuestion);

                    InfoSettingWindow wnd = obj as InfoSettingWindow;
                    wnd.Close();
                }));
            }
        }
        #endregion
        #region Property
        private int questionCollection;
        public int QuestionCollection
        {
            get => questionCollection;
            set
            {
                questionCollection = value;
                CountQuestion = QuestionCollection * CountQuestionOneType;
                OnPropertyChanged(nameof(QuestionCollection));
            }
        }
        private int countQuestionOneType;
        public int CountQuestionOneType
        {
            get => countQuestionOneType;
            set
            {
                countQuestionOneType = value;
                CountQuestion = QuestionCollection * CountQuestionOneType;
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
        #endregion
        public InfoSettingViewModel()
        {
            service = new JsonFileService();
            QuestionCollection = service.OpenQuestionCollection(fileInfo);
            CountQuestionOneType = service.OpenCountQuestionOneType(fileInfo);
            CountQuestion = service.OpenCountQuestion(fileInfo);
        }
    }
}
