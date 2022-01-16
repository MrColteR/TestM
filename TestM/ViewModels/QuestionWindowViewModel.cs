using System.Collections.ObjectModel;
using System.IO;
using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class QuestionWindowViewModel : ViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileName = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        private readonly string fileNewData = path.Substring(0, path.IndexOf("bin")) + "NewData.json";
        private readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";
        private readonly string fileSelectedQuestion = path.Substring(0, path.IndexOf("bin")) + "SelectedQuestion.json";

        private readonly JsonFileService fileService;
        private QuestionDataGridViewModel question;

        private bool checkWindowState = false;
        private bool IsEditing = false;
 
        #region Commands
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow => minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
        {
            QuestionWindow wnd = obj as QuestionWindow;
            wnd.WindowState = System.Windows.WindowState.Minimized;
        }));

        private RelayCommand windowStateButton;
        public RelayCommand WindowStateButton => windowStateButton ?? (windowStateButton = new RelayCommand(obj =>
        {
            QuestionWindow wnd = obj as QuestionWindow;
            if (checkWindowState)
            {
                wnd.WindowState = System.Windows.WindowState.Normal;
                checkWindowState = false;
            }
            else if (!checkWindowState)
            {
                wnd.WindowState = System.Windows.WindowState.Maximized;
                checkWindowState = true;
            }
        }));

        private RelayCommand closeWindow;
        public RelayCommand CloseWindow => closeWindow ?? (closeWindow = new RelayCommand(obj =>
        {
            if (!IsEditing)
            {
                QuestionWindow wnd = obj as QuestionWindow;
                wnd.Close();
            }
            else
            {
                ChangeDataWindow wnd = new ChangeDataWindow();
                wnd.ShowDialog();

                IsEditing = false;
                var check = fileService.OpenSaveFileCheck(fileInfo);

                if (check)
                {
                    RelayCommand relayCommand = SaveFile;
                    relayCommand.Execute(obj);
                }
                else
                {
                    RelayCommand relayCommand = CloseWindow;
                    relayCommand.Execute(obj);
                }
            }
        }));

        private RelayCommand addQuestion;
        public RelayCommand AddQuestion => addQuestion ?? (addQuestion = new RelayCommand(obj =>
        {
            AddQuestionWindow window = new AddQuestionWindow();
            window.ShowDialog();
            UpdateQuestionInDataGrid();
        }));

        private RelayCommand updateQuestion;
        public RelayCommand UpdateQuestion => updateQuestion ?? (updateQuestion = new RelayCommand(obj => 
        {
            QuestionWindow wnd = obj as QuestionWindow;
            QuestionModel model = (QuestionModel)wnd.DataGrid.SelectedItem;

            fileService.SaveSelectedQuestion(fileSelectedQuestion, new SelectedQuestion()
            {
                Question = model.Question,
                TypeQuestion = model.TypeQuestion,
                AnswerA = model.AnswerA,
                AnswerB = model.AnswerB,
                AnswerC = model.AnswerC,
                RightAnswer = model.RightAnswer,
                Index = wnd.DataGrid.SelectedIndex
            });

            UpdateQuestionWindow window = new UpdateQuestionWindow();
            window.ShowDialog();
            UpdateQuestionInDataGrid();
        }, (obj) => ItemsSource.Count > 0));

        private RelayCommand deleteQustion;
        public RelayCommand DeleteQuestion => deleteQustion ?? (deleteQustion = new RelayCommand(obj =>
        {
            IsEditing = true;
            var index = ItemsSource.IndexOf(SelectedItem);
            if (SelectedItem != null)
            {
                ItemsSource.Remove(SelectedItem);
                if (index == ItemsSource.Count)
                {
                    SelectedIndex = index - 1;
                }
                else
                {
                    SelectedIndex = index;
                }
            }
        }, (obj) => ItemsSource.Count > 0));

        private RelayCommand saveFile;
        public RelayCommand SaveFile => saveFile ?? (saveFile = new RelayCommand(obj =>
        {
            QuestionWindow wnd = obj as QuestionWindow;
            fileService.Save(fileName, ItemsSource);
            wnd.Close();
        }));

        private RelayCommand openSettingInfo;
        public RelayCommand OpenSettingInfo => openSettingInfo ?? (openSettingInfo = new RelayCommand(obj =>
        {
            InfoSettingWindow wnd = new InfoSettingWindow();
            wnd.ShowDialog();
        }));
        #endregion
        #region Property
        private QuestionModel selectedItem;
        public QuestionModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }
        private ObservableCollection<QuestionModel> itemsSource;
        public ObservableCollection<QuestionModel> ItemsSource
        {
            get => itemsSource;
            set 
            {
                itemsSource = value;
                OnPropertyChanged(nameof(ItemsSource));
            }
        }
        #endregion
        public QuestionWindowViewModel()
        {
            question = new QuestionDataGridViewModel();
            fileService = new JsonFileService();
            ItemsSource = fileService.Open(fileName);
        }
        private void UpdateQuestionInDataGrid()
        {
            ItemsSource = fileService.Open(fileName);
        }
    }
}
