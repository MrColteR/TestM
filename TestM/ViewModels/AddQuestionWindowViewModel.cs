using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class AddQuestionWindowViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        public readonly string fileName = path.Substring(0, path.IndexOf("bin")) + "Data.json";

        private JsonFileService fileService;

        private bool checkTypeButton = false;
        private bool checkAnswerButton = false;
        #region Commands
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow =>  minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
        {
            AddQuestionWindow wnd = obj as AddQuestionWindow;
            wnd.WindowState = System.Windows.WindowState.Minimized;
        }));

        private RelayCommand closeWindow;
        public RelayCommand CloseWindow => closeWindow ?? (closeWindow = new RelayCommand(obj =>
        {
            AddQuestionWindow wnd = obj as AddQuestionWindow;
            wnd.Close();
        }));

        private RelayCommand addQuestion;
        public RelayCommand AddQuestion => addQuestion ?? (addQuestion = new RelayCommand(obj =>
        {
            AddQuestionWindow wnd = obj as AddQuestionWindow;

            checkAnswerButton = false;
            checkTypeButton = false;
            bool isNull = false;

            if (wnd.Question.Text == "")
            {
                isNull = true;
                wnd.Question.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.Question.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (wnd.AnswerATextBox.Text == "")
            {
                isNull = true;
                wnd.AnswerATextBox.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.AnswerATextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (wnd.AnswerBTextBox.Text == "")
            {
                isNull = true;
                wnd.AnswerBTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.AnswerBTextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (wnd.AnswerCTextBox.Text == "")
            {
                isNull = true;
                wnd.AnswerCTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.AnswerCTextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (wnd.TypeButton.Content is null)
            {
                isNull = true;
                wnd.TypeButton.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.TypeButton.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (wnd.RightAnswerButton.Content is null)
            {
                isNull = true;
                wnd.RightAnswerButton.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.RightAnswerButton.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (!isNull)
            {
                Data.Add(new QuestionModel()
                {
                    Question = Question,
                    TypeQuestion = TypeQuestion,
                    AnswerA = AnswerA,
                    AnswerB = AnswerB,
                    AnswerC = AnswerC,
                    RightAnswer = RightAnswer
                });

                wnd.Question.Clear();
                wnd.Question.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

                wnd.AnswerATextBox.Clear();
                wnd.AnswerATextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

                wnd.AnswerBTextBox.Clear();
                wnd.AnswerBTextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

                wnd.AnswerCTextBox.Clear();
                wnd.AnswerCTextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

                wnd.TypeButton.Content = "";
                wnd.TypeButton.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

                wnd.RightAnswerButton.Content = "";
                wnd.TypeButton.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));
            }
        
        }));
        private RelayCommand close;
        public RelayCommand Close => close ?? (close = new RelayCommand(obj =>
        {
            AddQuestionWindow wnd = obj as AddQuestionWindow;
            wnd.Close();
        }));

        private RelayCommand openComboBoxType;
        public RelayCommand OpenComboBoxType => openComboBoxType ?? (openComboBoxType = new RelayCommand(obj =>
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

        private RelayCommand choiceType;
        public RelayCommand ChoiceType => choiceType ?? (choiceType = new RelayCommand(obj =>
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
            checkTypeButton = false;
        }));

        private RelayCommand openComboBoxAnswer;
        public RelayCommand OpenComboBoxAnswer => openComboBoxAnswer ?? (openComboBoxAnswer = new RelayCommand(obj =>
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

        private RelayCommand choiceAnswer;
        public RelayCommand ChoiceAnswer => choiceAnswer ?? (choiceAnswer = new RelayCommand(obj =>
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
            checkAnswerButton = false;
        }));
        #endregion
        #region Property
        private string question;
        private string typeQuestion;
        private string answerA;
        private string answerB;
        private string answerC;
        private string rightAnswer;
        public string Question
        {
            get => question;
            set => question = value;
        }
        public string TypeQuestion
        {
            get => typeQuestion;
            set => typeQuestion = value;
        }
        public string AnswerA
        {
            get => answerA;
            set => answerA = value;
        }
        public string AnswerB
        {
            get => answerB;
            set => answerB = value;
        }
        public string AnswerC
        {
            get => answerC;
            set => answerC = value;
        }
        public string RightAnswer
        {
            get => rightAnswer;
            set => rightAnswer = value;
        }
        public ObservableCollection<QuestionModel> Data { get; set; }
        #endregion
        public AddQuestionWindowViewModel(QuestionWindowViewModel data)
        {
            fileService = new JsonFileService();
            Data = data.ItemsSource;
        }
    }
}
