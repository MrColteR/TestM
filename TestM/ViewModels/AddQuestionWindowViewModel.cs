using System.Collections.ObjectModel;
using System.IO;
using TestM.Command;
using TestM.Data.Base;
using TestM.Models;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class AddQuestionWindowViewModel : ViewModel
    {
        IFileService fileService;
        private bool checkTypeButton = false;
        private bool checkAnswerButton = false;
        private static string path = Directory.GetCurrentDirectory();
        public readonly string fileName = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        #region Commands
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow
        {
            get
            {
                return minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
                {
                    AddQuestionWindow wnd = obj as AddQuestionWindow;
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
                    AddQuestionWindow wnd = obj as AddQuestionWindow;
                    wnd.Close();
                }));
            }
        }
        private RelayCommand save;
        public RelayCommand Save
        {
            get
            {
                return save ?? (save = new RelayCommand(obj =>
                {
                    ObservableCollection<QuestionModel> models = new ObservableCollection<QuestionModel>() { };
                    QuestionModel model = new QuestionModel();
                    Data.Add(new QuestionModel()
                    {
                        Question = Question,
                        TypeQuestion = TypeQuestion,
                        AnswerA = AnswerA,
                        AnswerB = AnswerB,
                        AnswerC = AnswerC,
                        AnswerD = AnswerD,
                        RightAnswer = RightAnswer
                    });
                }));
            }
        }
        private RelayCommand close;
        public RelayCommand Close 
        { 
            get
            {
                return close ?? (close = new RelayCommand(obj =>
                {
                    AddQuestionWindow wnd = obj as AddQuestionWindow;
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
                    AddQuestionWindow wnd = obj as AddQuestionWindow;
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
                    AddQuestionWindow wnd = obj as AddQuestionWindow;
                    wnd.GridComboBoxType.Visibility = System.Windows.Visibility.Hidden;
                    if (wnd.LegalBases.IsFocused)
                    {
                        wnd.TypeButton.Content = wnd.LegalBases.Content;
                        TypeQuestion = wnd.LegalBases.Content.ToString();
                    }
                    if (wnd.Safety.IsFocused)
                    {
                        wnd.TypeButton.Content = wnd.Safety.Content;
                        TypeQuestion = wnd.Safety.Content.ToString();
                    }
                    if (wnd.TTX.IsFocused)
                    {
                        wnd.TypeButton.Content = wnd.TTX.Content;
                        TypeQuestion = wnd.TTX.Content.ToString();
                    }
                    if (wnd.Command.IsFocused)
                    {
                        wnd.TypeButton.Content = wnd.Command.Content;
                        TypeQuestion = wnd.Command.Content.ToString();
                    }
                    if (wnd.Delays.IsFocused)
                    {
                        wnd.TypeButton.Content = wnd.Delays.Content;
                        TypeQuestion = wnd.Delays.Content.ToString();
                    }
                }));
            }
        }
        private RelayCommand openComboBoxAnswer;
        public RelayCommand OpenComboBoxAnswer
        {
            get
            {
                return openComboBoxAnswer ?? (openComboBoxAnswer = new RelayCommand(obj =>
                {
                    AddQuestionWindow wnd = obj as AddQuestionWindow;
                    if (!checkAnswerButton)
                    {
                        wnd.GridComboBoxRihgtAnswer.Visibility = System.Windows.Visibility.Visible;
                        checkAnswerButton = true;
                    }
                    else if (checkAnswerButton)
                    {
                        wnd.GridComboBoxRihgtAnswer.Visibility = System.Windows.Visibility.Hidden;
                        checkAnswerButton = false;
                    }
                }));
            }
        }
        private RelayCommand choiceAnswer;
        public RelayCommand ChoiceAnswer
        {
            get
            {
                return choiceAnswer ?? (choiceAnswer = new RelayCommand(obj =>
                {
                    AddQuestionWindow wnd = obj as AddQuestionWindow;
                    wnd.GridComboBoxRihgtAnswer.Visibility = System.Windows.Visibility.Hidden;
                    if (wnd.AnswerA.IsFocused)
                    {
                        wnd.RightAnswerButton.Content = wnd.AnswerA.Content;
                        RightAnswer = wnd.AnswerA.Content.ToString();
                    }
                    if (wnd.AnswerB.IsFocused)
                    {
                        wnd.RightAnswerButton.Content = wnd.AnswerB.Content;
                        RightAnswer = wnd.AnswerC.Content.ToString();
                    }
                    if (wnd.AnswerC.IsFocused)
                    {
                        wnd.RightAnswerButton.Content = wnd.AnswerC.Content;
                        RightAnswer = wnd.AnswerC.Content.ToString();
                    }
                    if (wnd.AnswerD.IsFocused)
                    {
                        wnd.RightAnswerButton.Content = wnd.AnswerD.Content;
                        RightAnswer = wnd.AnswerD.Content.ToString();
                    }
                }));
            }
        }
        #endregion
        #region Property
        private string question;
        private string typeQuestion;
        private string answerA;
        private string answerB;
        private string answerC;
        private string answerD;
        private string rightAnswer;
        public string Question
        {
            get { return question; }
            set
            {
                question = value;
            }
        }
        public string TypeQuestion
        {
            get 
            {
                return typeQuestion; 
            }
            set
            {
                typeQuestion = value;
            }
        }
        public string AnswerA
        {
            get { return answerA; }
            set
            {
                answerA = value;
            }
        }
        public string AnswerB
        {
            get { return answerB; }
            set
            {
                answerB = value;
            }
        }
        public string AnswerC
        {
            get { return answerC; }
            set
            {
                answerC = value;
            }
        }
        public string AnswerD
        {
            get { return answerD; }
            set
            {
                answerD = value;
            }
        }
        public string RightAnswer
        {
            get 
            {
                return rightAnswer; 
            }
            set
            {
                rightAnswer = value;
            }
        }
        public ObservableCollection<QuestionModel> Data { get; set; }
        #endregion
        public AddQuestionWindowViewModel(QuestionWindowViewModel data, IFileService fileService)
        {
            this.fileService = fileService;
            Data = data.ItemsSource;
        }
    }
}
