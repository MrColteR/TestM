using TestM.Command;
using TestM.Models;
using TestM.Views;

namespace TestM.ViewModels
{
    public class UpdateQuestionViewModel
    {
        private bool checkTypeButton = false;
        private bool checkAnswerButton = false;

        #region Commands
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow
        {
            get
            {
                return minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
                {
                    UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
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
                    UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
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
                    checkAnswerButton = false;
                    checkTypeButton = false;
                    UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
                    wnd.Close();
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
                    SelectedItem.Question = question;
                    SelectedItem.AnswerA = answerA;
                    SelectedItem.AnswerB = answerB;
                    SelectedItem.AnswerC = answerC;
                    SelectedItem.TypeQuestion = typeQuestion;
                    SelectedItem.RightAnswer = rightAnswer;
                    UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
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
                    UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
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
                    UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
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
                    checkTypeButton = false;
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
                    UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
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
                    UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
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
                    checkAnswerButton = false;
                }));
            }
        }
        #endregion
        #region Property
        private string question;
        private string answerA;
        private string answerB;
        private string answerC;
        private string typeQuestion;
        private string rightAnswer;
        public string Question
        {
            get => SelectedItem.Question;
            set => SelectedItem.Question = value;
        }
        public string TypeQuestion
        {
            get => SelectedItem.TypeQuestion;
            set => SelectedItem.TypeQuestion = value;
        }
        public string AnswerA
        {
            get => SelectedItem.AnswerA;
            set => SelectedItem.AnswerA = value;
        }
        public string AnswerB
        {
            get => SelectedItem.AnswerB;
            set => SelectedItem.AnswerB = value;
        }
        public string AnswerC
        {
            get => SelectedItem.AnswerC;
            set => SelectedItem.AnswerC = value;
        }
        public string RightAnswer
        {
            get => SelectedItem.RightAnswer;
            set => SelectedItem.RightAnswer = value;
        }
        public QuestionModel SelectedItem { get; set; }
        #endregion
        public UpdateQuestionViewModel(QuestionModel selectedItem)
        {
            SelectedItem = selectedItem;
            question = SelectedItem.Question;
            answerA = SelectedItem.AnswerA;
            answerB = SelectedItem.AnswerB;
            answerC = SelectedItem.AnswerC;
            typeQuestion = SelectedItem.TypeQuestion;
            rightAnswer = SelectedItem.RightAnswer;
        }
    }
}
