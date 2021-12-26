using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        StartPageTest startPageTest;
        List<PageTest> pages;
        LastPageTest lastPageTest;

        ObservableCollection<QuestionModel> questionsList;
        ObservableCollection<QuestionModel> actualQuestions;

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
                }));
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
                }));
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
                    foreach (var item in pages)
                    {
                        answerUser.Add(item.RigntAnswer.Text);
                    }
                    CurrentPage = lastPageTest;
                }));
            }
        }
        #endregion
        #region Property
        private Page currentPage;
        public Page CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        #endregion
        public MainWindowViewModel()
        {
            actualQuestions = new ObservableCollection<QuestionModel>();
            answerUser = new List<string>();

            startPageTest = new StartPageTest();
            lastPageTest = new LastPageTest();

            pages = new List<PageTest>();
            for (int i = 0; i < 20; i++)
            {
                pages.Add(new PageTest());
            }

            JsonFileService service = new JsonFileService();
            service.SaveIndexFirst(fileIndex, new ActualQuestion());

            SortList();
            SaveRandomQuestion();
        }
        private void SortList()
        {
            List<ObservableCollection<QuestionModel>> questionCollection = new List<ObservableCollection<QuestionModel>>();
            for (int i = 0; i < 5; i++)
            {
                questionCollection.Add(new ObservableCollection<QuestionModel>());
            }

            JsonFileService jsonFile = new JsonFileService();
            questionsList = jsonFile.Open(fileData);

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
            }
        }
        private void SaveRandomQuestion()
        {
            JsonFileService service = new JsonFileService();
            service.Save(fileActualQuestion, actualQuestions);
        }
    }
}
