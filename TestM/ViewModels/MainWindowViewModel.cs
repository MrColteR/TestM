using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
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
        private static string path = Directory.GetCurrentDirectory();
        private readonly string fileData = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        private readonly string fileActualQuestion = path.Substring(0, path.IndexOf("bin")) + "ActualQuestion.json";
        private readonly string fileIndex = path.Substring(0, path.IndexOf("bin")) + "Index.json";

        JsonFileService service;

        StartPageTest startPageTest;
        List<PageTest> pages;
        LastPageTest lastPageTest;

        ObservableCollection<QuestionModel> questionsList;
        ObservableCollection<QuestionModel> actualQuestions;
        List<string> rightAnswer;
        List<string> answerUser;

        int indexPage;
        bool IsStart;
        bool IsLastPage;

        #region Command
        private RelayCommand checkPassword;
        public RelayCommand CheckPassword 
        { 
            get
            {
                return checkPassword ?? (checkPassword = new RelayCommand(obj =>
                {
                    var password = new PasswordWindow();
                    password.ShowDialog();
                }, (obj) =>  IsStart == false));
            }
            
        }
        private RelayCommand openSetting;
        public RelayCommand OpenSetting
        {
            get
            {
                return openSetting ?? (openSetting = new RelayCommand(obj =>
                {
                    SettingWindow wnd = new SettingWindow();
                    wnd.Show();
                }, (obj) =>  IsStart == false));
            }
        }
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow
        {
            get
            {
                return minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
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
                    MainWindow wnd = obj as MainWindow;
                    wnd.Close();
                }));
            }
        }
        private RelayCommand startTest;
        public RelayCommand StartTest
        {
            get
            {
                return startTest ?? (startTest = new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
                    wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Visible;
                    wnd.NextPageButton.Visibility = System.Windows.Visibility.Visible;

                    CurrentPage = startPageTest;
                    IsStart = true;

                }, (obj) =>  IsStart == false));
            }
        }
        private RelayCommand previousPage;
        public RelayCommand PreviousPage
        {
            get
            {
                return previousPage ?? (previousPage = new RelayCommand(obj => 
                {
                    MainWindow wnd = obj as MainWindow;
                    indexPage--;
                    CurrentPage = pages[indexPage];
                    if (IsLastPage)
                    {
                        wnd.EndTestButton.Visibility = System.Windows.Visibility.Hidden;
                        wnd.NextPageButton.Visibility = System.Windows.Visibility.Visible;
                        IsLastPage = false;
                    }
                    
                }, (obj) => indexPage > 1));
            }
        }
        private RelayCommand nextPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ?? (nextPage = new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
                    if (indexPage == 0)
                    {
                        AddResultToFile.WriteName(startPageTest.NameTextBox.Text, startPageTest.SubdivisionTextBox.Text, DateTime.Now.Date.ToShortDateString());
                    }
                    indexPage++;
                    CurrentPage = pages[indexPage];

                    if (indexPage == pages.Count - 1)
                    {
                        IsLastPage = true;
                        wnd.NextPageButton.Visibility = System.Windows.Visibility.Hidden;
                        wnd.EndTestButton.Visibility = System.Windows.Visibility.Visible;
                    }

                }, (obj) => indexPage < pages.Count - 1));
            }
        }
        private RelayCommand scoring;
        public RelayCommand Scoring
        {
            get
            {
                return scoring ?? (scoring = new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
                    wnd.EndTestButton.Visibility = System.Windows.Visibility.Hidden;
                    wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Hidden;
                    wnd.StartNewTestButton.Visibility = System.Windows.Visibility.Visible;

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
                    AddResultToFile.WritePoints(CountPoints.ToString());
                    if (CountPoints >= 17)
                    {
                        lastPageTest.Result.Text = "Вы успешно прошли тест";
                    }
                    else {
                        lastPageTest.Result.Text = $"Вы не прошли тест, Вам не хватило {17 - CountPoints} баллов";
                    }
                    CurrentPage = lastPageTest;
                }));
            }
        }
        private RelayCommand startNewTest;
        public RelayCommand StartNewTest
        {
            get 
            {
                return startNewTest ?? (startNewTest = new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
                    wnd.NextPageButton.Visibility = System.Windows.Visibility.Visible;
                    wnd.PreviousPageButton.Visibility = System.Windows.Visibility.Visible;
                    wnd.StartNewTestButton.Visibility = System.Windows.Visibility.Hidden;
                    startPageTest.NameTextBox.Text = "";
                    startPageTest.SubdivisionTextBox.Text = "";

                    
                    indexPage = 0;
                    actualQuestions.Clear();
                    rightAnswer.Clear();
                    answerUser.Clear();

                    SortList();
                    SaveRandomQuestion();
                    startPageTest = new StartPageTest();
                    pages = new List<PageTest>();
                    for (int i = 0; i < 20; i++)
                    {
                        pages.Add(new PageTest());
                    }
                    lastPageTest = new LastPageTest();
                    CurrentPage = startPageTest;
                }));
            }
        }
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

            SortList();
            SaveRandomQuestion();

            startPageTest = new StartPageTest();
            pages = new List<PageTest>();
            for (int i = 0; i < 20; i++)
            {
                pages.Add(new PageTest());
            }
            lastPageTest = new LastPageTest();

            service = new JsonFileService();
            service.SaveIndexFirst(fileIndex, new ActualQuestion());
        }
        private void SortList()
        {
            List<ObservableCollection<QuestionModel>> questionCollection = new List<ObservableCollection<QuestionModel>>();
            for (int i = 0; i < 5; i++)
            {
                questionCollection.Add(new ObservableCollection<QuestionModel>());
            }

            service = new JsonFileService();
            questionsList = service.Open(fileData);

            List<string> types = new List<string>()
            {
                "Меры безопастности",
                "Правовые основания",
                "ТТХ",
                "Команды",
                "Задержки"
            };
            
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
            for (int i = 0; i < 4; i++)
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
