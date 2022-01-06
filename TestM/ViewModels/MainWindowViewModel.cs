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
        }, (obj) =>  IsStart == false));

        private RelayCommand openSetting;
        public RelayCommand OpenSetting => openSetting ?? (openSetting = new RelayCommand(obj =>
        {
            SettingWindow wnd = new SettingWindow();
            wnd.ShowDialog();
        }, (obj) =>  IsStart == false));

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
            if (IsStart && !closeTest)
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
                wndMain.Close();
            }
        }));

        private RelayCommand startTest;
        public RelayCommand StartTest => startTest ?? (startTest = new RelayCommand(obj =>
        {
            info = service.OpenInfo(fileInfo);
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
            wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Visible;
            wnd.NextPageButton.Visibility = System.Windows.Visibility.Visible;

            CurrentPage = startPageTest;
            IsStart = true;
        }, (obj) =>  IsStart == false));

        private RelayCommand stopTest;
        public RelayCommand StopTest => stopTest ?? (stopTest = new RelayCommand(obj => 
        {
            StopTestWindow wnd = new StopTestWindow();
            wnd.ShowDialog();

            var check = service.OpenStopCheckCheck(fileInfo);

            if (check)
            {
                RelayCommand relayCommand = StartNewTest;
                relayCommand.Execute(obj);
            }
        }));

        private RelayCommand previousPage;
        public RelayCommand PreviousPage => previousPage ?? (previousPage = new RelayCommand(obj => 
        {
            MainWindow wnd = obj as MainWindow;
            indexPage--;
            CurrentPage = pages[indexPage];
            if (IsLastPage)
            {
                wnd.EndTestButton.Visibility = System.Windows.Visibility.Hidden;
                wnd.NextPageButton.Visibility = System.Windows.Visibility.Visible;
                wnd.StopTestButton.Visibility = System.Windows.Visibility.Visible;
                IsLastPage = false;
            }

        }, (obj) => indexPage > 0));

        private RelayCommand nextPage;
        public RelayCommand NextPage => nextPage ?? (nextPage = new RelayCommand(obj =>
        {
            if (!IsPassedTheCheck)
            {
                IsStartPageTextBoolIsNull = false;
                if (startPageTest.NameTextBox.Text == "")
                {
                    startPageTest.NameTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
                    IsStartPageTextBoolIsNull = true;
                }
                else
                    startPageTest.NameTextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));
                if (startPageTest.SubdivisionTextBox.Text == "")
                {
                    startPageTest.SubdivisionTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
                    IsStartPageTextBoolIsNull = true;
                }
                else
                    startPageTest.SubdivisionTextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            }
            if (!IsStartPageTextBoolIsNull)
            {
                MainWindow wnd = obj as MainWindow;
                if (indexPage == -1)
                {
                    wnd.StopTestButton.Visibility = System.Windows.Visibility.Visible;
                }
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
        }, (obj) => indexPage<pages.Count - 1));

        private RelayCommand scoring;
        public RelayCommand Scoring => scoring ?? (scoring = new RelayCommand(obj =>
        {
            MainWindow wnd = obj as MainWindow;
            wnd.EndTestButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.StartNewTestButton.Visibility = System.Windows.Visibility.Visible;

            int points = Convert.ToInt32(service.OpenMinimalCountPonits(fileInfo));

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
            }

            lastPageTest.Points.Text = CountPoints.ToString();
            AddResultToFile.WriteName(startPageTest.NameTextBox.Text,
                                      startPageTest.SubdivisionTextBox.Text,
                                      DateTime.Now.Date.ToShortDateString());
            AddResultToFile.WritePoints(CountPoints.ToString());
            if (CountPoints >= points)
            {
                lastPageTest.Result.Text = $"Вы успешно прошли тест, Вы набрали {CountPoints}";
            }
            else
            {
                lastPageTest.Result.Text = $"Вы не прошли тест, Вам не хватило {points - CountPoints} баллов до минимального порога";
            }
            CurrentPage = lastPageTest;
        }));
        private RelayCommand startNewTest;
        public RelayCommand StartNewTest => startNewTest ?? (startNewTest = new RelayCommand(obj =>
        {
            MainWindow wnd = obj as MainWindow;
            wnd.NextPageButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.StartNewTestButton.Visibility = System.Windows.Visibility.Hidden;
            wnd.StopTestButton.Visibility = System.Windows.Visibility.Hidden;
            startPageTest.NameTextBox.Text = "";
            startPageTest.SubdivisionTextBox.Text = "";

            IsStart = false;
            indexPage = -1;
            countPoints = 0;
            actualQuestions.Clear();
            rightAnswer.Clear();
            answerUser.Clear();

            service.SaveFirstIndex(fileInfo);
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
        #endregion
        public MainWindowViewModel()
        {
            actualQuestions = new ObservableCollection<QuestionModel>();
            rightAnswer = new List<string>();
            answerUser = new List<string>();
            service = new JsonFileService();

            indexPage = -1;
            countPoints = 0;

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
    }
}
