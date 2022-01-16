using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.Pages;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileData = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        private readonly string fileActualQuestion = path.Substring(0, path.IndexOf("bin")) + "ActualQuestion.json";
        private readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";

        private JsonFileService service;
        private Info info;

        private MainPage mainPage;
        private StartPageTest startPageTest;
        private List<PageTest> pages;
        private LastPageTest lastPageTest;

        private ObservableCollection<QuestionModel> questionsList;
        private ObservableCollection<QuestionModel> actualQuestions;

        private List<string> rightAnswer;
        private List<string> answerUser;

        private int indexPage;
        private bool IsStart;
        private bool IsBeClose;
        private bool IsLastPage;
        private bool checkWindowState = false;
        private bool IsStartPageTextBoolIsNull;
        private bool IsPassedTheCheck = false;
        private bool closeTest = false;

        #region Command
        private RelayCommand checkPassword;
        public RelayCommand CheckPassword => checkPassword ?? (checkPassword = new RelayCommand(obj =>
        {
            var password = new PasswordWindow();
            password.ShowDialog();
        }, (obj) => !IsStart));

        private RelayCommand openSetting;
        public RelayCommand OpenSetting => openSetting ?? (openSetting = new RelayCommand(obj =>
        {
            SettingWindow wnd = new SettingWindow();
            wnd.ShowDialog();
        }, (obj) => !IsStart));

        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow => minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
        {
            MainWindow wnd = obj as MainWindow;
            wnd.WindowState = System.Windows.WindowState.Minimized;
        }));

        private RelayCommand windowStateButton;
        public RelayCommand WindowStateButton => windowStateButton ?? (windowStateButton = new RelayCommand(obj =>
        {
            MainWindow wnd = obj as MainWindow;
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
            if (IsBeClose && !closeTest)
            {
                StopTestWindow wnd = new StopTestWindow();
                wnd.ShowDialog();

                var check = service.OpenStopCheckCheck(fileInfo);
                if (check)
                {
                    closeTest = true;
                    RelayCommand relayCommand = CloseWindow;
                    relayCommand.Execute(obj);
                }
            }
            else
            {
                MainWindow wndMain = obj as MainWindow;
                service.SaveCountOfQuestionsAnswered(fileInfo, 0);
                wndMain.Close();
            }
        }));

        private RelayCommand startTest;
        public RelayCommand StartTest => startTest ?? (startTest = new RelayCommand(obj =>
        {
            info = service.OpenInfo(fileInfo);
            CountOfQuestionsAnswered = service.OpenCountOfQuestionsAnswered(fileInfo);
            CountQuestion = service.OpenCountQuestion(fileInfo);
            SortList();
            SaveRandomQuestion();

            startPageTest = new StartPageTest();
            pages = new List<PageTest>();
            for (int i = 0; i < info.CountQuestion; i++)
            {
                pages.Add(new PageTest());
            }
            lastPageTest = new LastPageTest();

            MainWindow wnd = obj as MainWindow;
            wnd.TestButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.EditingButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.SettingButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.ProgressBar.Visibility = System.Windows.Visibility.Visible;
            wnd.NextPageButton.Visibility = System.Windows.Visibility.Visible;

            CurrentPage = startPageTest;
            IsStart = true;
            IsBeClose = true;
        }, (obj) => !IsStart));

        private RelayCommand stopTest;
        public RelayCommand StopTest => stopTest ?? (stopTest = new RelayCommand(obj =>
        {
            StopTestWindow wnd = new StopTestWindow();
            wnd.ShowDialog();

            var check = service.OpenStopCheckCheck(fileInfo);

            if (check)
            {
                IsBeClose = false;
                RelayCommand relayCommand = BackToMainPage;
                relayCommand.Execute(obj);
            }
        }));

        private RelayCommand previousPage;
        public RelayCommand PreviousPage => previousPage ?? (previousPage = new RelayCommand(obj =>
        {
            MainWindow wnd = obj as MainWindow;
            indexPage--;
            CountOfQuestionsAnswered = service.OpenCountOfQuestionsAnswered(fileInfo);
            CurrentPage = pages[indexPage];
            if (indexPage == 0)
            {
                wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Hidden;
            }

            if (IsLastPage)
            {
                wnd.EndTestButton.Visibility = System.Windows.Visibility.Hidden;
                wnd.NextPageButton.Visibility = System.Windows.Visibility.Visible;
                wnd.StopTestButton.Visibility = System.Windows.Visibility.Visible;
                IsLastPage = false;
            }

        }, (obj) => indexPage > 0 && IsStart));

        private RelayCommand nextPage;
        public RelayCommand NextPage => nextPage ?? (nextPage = new RelayCommand(obj =>
        {
            if (!IsPassedTheCheck)
            {
                CheckTextBoxIsNull(startPageTest.NameTextBox);
                CheckTextBoxIsNull(startPageTest.SubdivisionTextBox);

                if (IsPassedTheCheck)
                {
                    IsStartPageTextBoolIsNull = true;
                    EmptyFieldsWindow window = new EmptyFieldsWindow();
                    window.ShowDialog();
                }
                else
                {
                    IsStartPageTextBoolIsNull = false;
                }
                IsPassedTheCheck = false;
            }
            if (!IsStartPageTextBoolIsNull)
            {
                MainWindow wnd = obj as MainWindow;
                if (indexPage == -1)
                {
                    wnd.StopTestButton.Visibility = System.Windows.Visibility.Visible;
                }

                if (indexPage == 0)
                {
                    wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Visible;
                }

                CountOfQuestionsAnswered = service.OpenCountOfQuestionsAnswered(fileInfo);
                indexPage++;
                CurrentPage = pages[indexPage];

                if (indexPage == pages.Count - 1)
                {
                    IsLastPage = true;
                    wnd.NextPageButton.Visibility = System.Windows.Visibility.Hidden;
                    wnd.EndTestButton.Visibility = System.Windows.Visibility.Visible;
                    wnd.StopTestButton.Visibility = System.Windows.Visibility.Hidden;
                }
                IsPassedTheCheck = true;
            }
        }, (obj) => indexPage < pages.Count - 1 && IsStart));

        private RelayCommand scoring;
        public RelayCommand Scoring => scoring ?? (scoring = new RelayCommand(obj =>
        {
            MainWindow wnd = obj as MainWindow;
            wnd.TestButton.Visibility = System.Windows.Visibility.Visible;
            wnd.EditingButton.Visibility = System.Windows.Visibility.Visible;
            wnd.SettingButton.Visibility = System.Windows.Visibility.Visible;
            wnd.StartNewTestButton.Visibility = System.Windows.Visibility.Visible;
            wnd.EndTestButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.ProgressBar.Visibility = System.Windows.Visibility.Hidden;

            var numberQuestion = 1;
            int points = Convert.ToInt32(service.OpenMinimalCountPonits(fileInfo));
            int maxCountPoints = service.OpenCountQuestion(fileInfo);
            service.SaveCountOfQuestionsAnswered(fileInfo, 0);
            foreach (var item in pages)
            {
                answerUser.Add(item.RigntAnswer.Text);
            }
            for (int i = 0; i < answerUser.Count; i++)
            {
                if (answerUser[i] == rightAnswer[i])
                {
                    CountPoints++;
                }
                else
                {
                    var style = service.OpenStyleApp(fileInfo);

                    CreateTextBlockOnLastPage(new TextBlock(), actualQuestions[i], style, i, numberQuestion, 18);
                    CreateTextBlockOnLastPage(new TextBlock(), actualQuestions[i], style, i, numberQuestion, 16, "А");
                    CreateTextBlockOnLastPage(new TextBlock(), actualQuestions[i], style, i, numberQuestion, 16, "Б");
                    CreateTextBlockOnLastPage(new TextBlock(), actualQuestions[i], style, i, numberQuestion, 16, "В");

                    numberQuestion++;
                }
            }

            AddResultToFile.WriteName(startPageTest.NameTextBox.Text,
                                      startPageTest.SubdivisionTextBox.Text,
                                      DateTime.Now.Date.ToShortDateString());
            AddResultToFile.WritePoints(CountPoints.ToString());
            lastPageTest.Result.Text = CountPoints >= points ? $"Вы успешно прошли тест, Вы набрали {CountPoints} из {maxCountPoints}."
                : $"Вы не прошли тест, Вам не хватило {points - CountPoints} баллов до минимального порога.";
            lastPageTest.ResultPoins.Text = CountPoints == 20 ? "Вы не допустили ошибкок!" : "Вопросы в которых Вы допустили ошибки:";

            IsBeClose = false;
            IsLastPage = false;
            CurrentPage = lastPageTest;
        }, (obj) => IsLastPage));
        private RelayCommand backToMainPage;
        public RelayCommand BackToMainPage => backToMainPage ?? (backToMainPage = new RelayCommand(obj =>
        {
            MainWindow wnd = obj as MainWindow;
            wnd.NextPageButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.StartNewTestButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.StopTestButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.ProgressBar.Visibility = System.Windows.Visibility.Hidden;
            wnd.TestButton.Visibility = System.Windows.Visibility.Visible;
            wnd.EditingButton.Visibility = System.Windows.Visibility.Visible;
            wnd.SettingButton.Visibility = System.Windows.Visibility.Visible;

            startPageTest.NameTextBox.Text = "";
            startPageTest.SubdivisionTextBox.Text = "";

            IsPassedTheCheck = false;
            IsStartPageTextBoolIsNull = true;
            indexPage = -1;
            countPoints = 0;
            IsStart = false;
            actualQuestions.Clear();
            rightAnswer.Clear();
            answerUser.Clear();

            service.SaveFirstIndex(fileInfo);
            service.SaveCountOfQuestionsAnswered(fileInfo, 0);
            CurrentPage = mainPage;
        }));
        #endregion
        #region Property
        private Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        private string name;
        public string Name
        {
            get => name;
            set 
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string subdivision;
        public string Subdivision
        {
            get => subdivision;
            set
            {
                subdivision = value;
                OnPropertyChanged(nameof(Subdivision));
            }
        }
        private string dateTest;
        public string DateTest
        {
            get => dateTest;
            set
            {
                dateTest = value;
                OnPropertyChanged(nameof(DateTest));
            }
        }
        private int countPoints;
        public int CountPoints
        {
            get => countPoints;
            set
            {
                countPoints = value;
                OnPropertyChanged(nameof(CountPoints));
            }
        }
        private int countOfQuestionsAnswered;
        public int CountOfQuestionsAnswered
        {
            get => countOfQuestionsAnswered; 
            set 
            {
                countOfQuestionsAnswered = value;
                OnPropertyChanged(nameof(CountOfQuestionsAnswered));
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
        public MainWindowViewModel()
        {
            actualQuestions = new ObservableCollection<QuestionModel>();
            rightAnswer = new List<string>();
            answerUser = new List<string>();
            service = new JsonFileService();

            indexPage = -1;
            countPoints = 0;
            IsPassedTheCheck = false;
            IsStartPageTextBoolIsNull = true;

            mainPage = new MainPage();
            pages = new List<PageTest>();
            CurrentPage = mainPage;

            service.SaveFirstIndex(fileInfo);
        }

        private void SortList()
        {
            service = new JsonFileService();
            questionsList = service.Open(fileData);

            Dictionary<int, bool> stateType = service.OpenButtonStates(fileInfo);
            List<string> types = new List<string>();

            foreach (var item in stateType)
            {
                if (item.Value == true)
                {
                    types.Add(item.Key.ToString());
                }
            }

            List<ObservableCollection<QuestionModel>> questionCollection = new List<ObservableCollection<QuestionModel>>();
            for (int i = 0; i < types.Count; i++)
            {
                questionCollection.Add(new ObservableCollection<QuestionModel>());
            }
                        
            for (int i = 0; i < questionCollection.Count; i++)
            {
                questionCollection[i] = SortType(types[i], questionsList);
            }
            
            foreach (var item in questionCollection)
            {
                GetRandomQuestion(item);
            }
        }
        private ObservableCollection<QuestionModel> SortType(string type, ObservableCollection<QuestionModel> list)
        {
            ObservableCollection<QuestionModel> sortList = new ObservableCollection<QuestionModel>();
            foreach (var item in list)
            {
                if (item.TypeQuestion == type)
                {
                    sortList.Add(item);
                }
            }
            return sortList;
        }

        private void GetRandomQuestion(ObservableCollection<QuestionModel> questions)
        {
            Random random = new Random();
            int[] numb = Enumerable.Range(0, questions.Count).ToArray();
            for (int i = numb.Length - 1; i > 0; i--)
            {
                int j = random.Next(i);
                int t = numb[i];
                numb[i] = numb[j];
                numb[j] = t;
            }
            for (int i = 0; i < info.CountQuestionOneType; i++)
            {
                actualQuestions.Add(questions[numb[i]]);
                rightAnswer.Add(questions[numb[i]].RightAnswer);
            }
        }

        private void SaveRandomQuestion()
        {
            service = new JsonFileService();
            service.Save(fileActualQuestion, actualQuestions);
        }

        private void CreateTextBlockOnLastPage(TextBlock textBlock, QuestionModel model, string style, int index, int numberQuestion, int fontSize, string answer = "")
        {
            lastPageTest.AllResult.Children.Add(textBlock);
            textBlock.TextWrapping = System.Windows.TextWrapping.Wrap;
            textBlock.FontSize = fontSize;
            textBlock.Margin = new System.Windows.Thickness(10, 5, 25, 0);
            textBlock.LineHeight = 5;
            if (style == "Темная")
                textBlock.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 172));
            
            switch (answer)
            {
                case "":
                    textBlock.Text = $"{numberQuestion}) " + $"{actualQuestions[index].Question}";
                    textBlock.Margin = new System.Windows.Thickness(10, 25, 25, 0);
                    break;
                case "А":
                    textBlock.Text = "а) " + $"{actualQuestions[index].AnswerA}";
                    ChangeBackgroundColor(textBlock, actualQuestions[index].RightAnswer, answerUser[index], answer);
                    break;
                case "Б":
                    textBlock.Text = "б) " + $"{actualQuestions[index].AnswerB}";
                    ChangeBackgroundColor(textBlock, actualQuestions[index].RightAnswer, answerUser[index], answer);
                    break;
                case "В":
                    textBlock.Text = "в) " + $"{actualQuestions[index].AnswerC}";
                    ChangeBackgroundColor(textBlock, actualQuestions[index].RightAnswer, answerUser[index], answer);
                    break;
            }
        }

        private void ChangeBackgroundColor(TextBlock textBlock, string rightAnswer, string answerUser, string answer)
        {
            if (rightAnswer == answer)
                textBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 128, 0));
            if (answerUser == answer)
                textBlock.Foreground = new SolidColorBrush(Color.FromRgb(210, 4, 45));
        }

        private void CheckTextBoxIsNull(TextBox obj)
        {
            if (obj.Text == "")
            {
                IsPassedTheCheck = true;
            }
        }
    }
}
