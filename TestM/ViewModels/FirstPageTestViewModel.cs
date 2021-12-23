using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.Pages;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    public class FirstPageTestViewModel : ViewModel
    {
        MainWindow window;
        ObservableCollection<QuestionModel> questionsOldList;
        ObservableCollection<QuestionModel> questionSortList = new ObservableCollection<QuestionModel>();
        private static string path = Directory.GetCurrentDirectory();
        private readonly string filePath = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        private int questionFirst;
        private int questionSecond;
        private int questionThird;
        private int questionFourth;
        private string answerFirst;
        private string answerSecond;
        private string answerThird;
        private string answerFourth;
        private bool checkAnswerFirstButton = false;
        private bool checkAnswerSecondButton = false;
        private bool checkAnswerThirdButton = false;
        private bool checkAnswerFourthButton = false;
        private int resultPoints = 0;
        #region Command
        private RelayCommand firstComboBox;
        public RelayCommand FirstComboBox
        {
            get 
            {
                return firstComboBox ?? (firstComboBox = new RelayCommand(obj => { 
                    FirstPageTest page = obj as FirstPageTest;
                    if (!checkAnswerFirstButton)
                    {
                        page.GridComboBoxRihgtAnswerFirst.Visibility = System.Windows.Visibility.Visible;
                        checkAnswerFirstButton = true;
                    }
                    else if (checkAnswerFirstButton)
                    {
                        page.GridComboBoxRihgtAnswerFirst.Visibility = System.Windows.Visibility.Hidden;
                        checkAnswerFirstButton = false;
                    }
                })); 
            }
        }
        private RelayCommand secondComboBox;
        public RelayCommand SecondComboBox
        {
            get
            {
                return secondComboBox ?? (secondComboBox = new RelayCommand(obj => {
                    FirstPageTest page = obj as FirstPageTest;
                    if (!checkAnswerSecondButton)
                    {
                        page.GridComboBoxRihgtAnswerSecond.Visibility = System.Windows.Visibility.Visible;
                        checkAnswerSecondButton = true;
                    }
                    else if (checkAnswerSecondButton)
                    {
                        page.GridComboBoxRihgtAnswerSecond.Visibility = System.Windows.Visibility.Hidden;
                        checkAnswerSecondButton = false;
                    }
                }));
            }
        }
        private RelayCommand thirdComboBox;
        public RelayCommand ThirdComboBox
        {
            get
            {
                return thirdComboBox ?? (thirdComboBox = new RelayCommand(obj => {
                    FirstPageTest page = obj as FirstPageTest;
                    if (!checkAnswerThirdButton)
                    {
                        page.GridComboBoxRihgtAnswerThird.Visibility = System.Windows.Visibility.Visible;
                        checkAnswerThirdButton = true;
                    }
                    else if (checkAnswerThirdButton)
                    {
                        page.GridComboBoxRihgtAnswerThird.Visibility = System.Windows.Visibility.Hidden;
                        checkAnswerThirdButton = false;
                    }
                }));
            }
        }
        private RelayCommand fourthComboBox;
        public RelayCommand FourthComboBox
        {
            get
            {
                return fourthComboBox ?? (fourthComboBox = new RelayCommand(obj => {
                    FirstPageTest page = obj as FirstPageTest;
                    if (!checkAnswerFourthButton)
                    {
                        page.GridComboBoxRihgtAnswerFourth.Visibility = System.Windows.Visibility.Visible;
                        checkAnswerFourthButton = true;
                    }
                    else if (checkAnswerFourthButton)
                    {
                        page.GridComboBoxRihgtAnswerFourth.Visibility = System.Windows.Visibility.Hidden;
                        checkAnswerFourthButton = false;
                    }
                }));
            }
        }
        private RelayCommand choiceAnswerFirst;
        public RelayCommand ChoiceAnswerFirst
        {
            get
            {
                return choiceAnswerFirst ?? (choiceAnswerFirst = new RelayCommand(obj =>
                {
                    FirstPageTest page = obj as FirstPageTest;
                    page.GridComboBoxRihgtAnswerFirst.Visibility = System.Windows.Visibility.Hidden;
                    if (page.AnswerFirstA.IsFocused)
                    {
                        page.QuestionFirstButton.Content = page.AnswerFirstA.Content;
                        AnswerFirst = "А";
                    }
                    if (page.AnswerFirstB.IsFocused)
                    {
                        page.QuestionFirstButton.Content = page.AnswerFirstB.Content;
                        AnswerFirst = "Б";
                    }
                    if (page.AnswerFirstC.IsFocused)
                    {
                        page.QuestionFirstButton.Content = page.AnswerFirstC.Content;
                        AnswerFirst = "В";
                    }
                    if (page.AnswerFirstD.IsFocused)
                    {
                        page.QuestionFirstButton.Content = page.AnswerFirstD.Content;
                        AnswerFirst = "Г";
                    }
                    checkAnswerFirstButton = false;
                }));
            }
        }
        private RelayCommand choiceAnswerSecond;
        public RelayCommand ChoiceAnswerSecond
        {
            get
            {
                return choiceAnswerSecond ?? (choiceAnswerSecond = new RelayCommand(obj =>
                {
                    FirstPageTest page = obj as FirstPageTest;
                    page.GridComboBoxRihgtAnswerSecond.Visibility = System.Windows.Visibility.Hidden;
                    if (page.AnswerSecondA.IsFocused)
                    {
                        page.QuestionSecondButton.Content = page.AnswerSecondA.Content;
                        AnswerSecond = "А";
                    }
                    if (page.AnswerSecondB.IsFocused)
                    {
                        page.QuestionSecondButton.Content = page.AnswerSecondB.Content;
                        AnswerSecond = "Б";
                    }
                    if (page.AnswerSecondC.IsFocused)
                    {
                        page.QuestionSecondButton.Content = page.AnswerSecondC.Content;
                        AnswerSecond = "В";
                    }
                    if (page.AnswerSecondD.IsFocused)
                    {
                        page.QuestionSecondButton.Content = page.AnswerSecondD.Content;
                        AnswerSecond = "Г";
                    }
                    checkAnswerSecondButton = false;
                }));
            }
        }
        private RelayCommand choiceAnswerThird;
        public RelayCommand ChoiceAnswerThird
        {
            get
            {
                return choiceAnswerThird ?? (choiceAnswerThird = new RelayCommand(obj =>
                {
                    FirstPageTest page = obj as FirstPageTest;
                    page.GridComboBoxRihgtAnswerThird.Visibility = System.Windows.Visibility.Hidden;
                    if (page.AnswerThirdA.IsFocused)
                    {
                        page.QuestionThirdButton.Content = page.AnswerThirdA.Content;
                        AnswerThird = "А";
                    }
                    if (page.AnswerThirdB.IsFocused)
                    {
                        page.QuestionThirdButton.Content = page.AnswerThirdB.Content;
                        AnswerThird = "Б";
                    }
                    if (page.AnswerThirdC.IsFocused)
                    {
                        page.QuestionThirdButton.Content = page.AnswerThirdC.Content;
                        AnswerThird = "В";
                    }
                    if (page.AnswerThirdD.IsFocused)
                    {
                        page.QuestionThirdButton.Content = page.AnswerThirdD.Content;
                        AnswerThird = "Г";
                    }
                    checkAnswerThirdButton = false;
                }));
            }
        }
        private RelayCommand choiceAnswerFourth;
        public RelayCommand ChoiceAnswerFourth
        {
            get
            {
                return choiceAnswerFourth ?? (choiceAnswerFourth = new RelayCommand(obj =>
                {
                    FirstPageTest page = obj as FirstPageTest;
                    page.GridComboBoxRihgtAnswerFourth.Visibility = System.Windows.Visibility.Hidden;
                    if (page.AnswerFourthA.IsFocused)
                    {
                        page.QuestionFourthButton.Content = page.AnswerFourthA.Content;
                        AnswerFourth = "А";
                    }
                    if (page.AnswerFourthB.IsFocused)
                    {
                        page.QuestionFourthButton.Content = page.AnswerFourthB.Content;
                        AnswerFourth = "Б";
                    }
                    if (page.AnswerFourthC.IsFocused)
                    {
                        page.QuestionFourthButton.Content = page.AnswerFourthC.Content;
                        AnswerFourth = "В";
                    }
                    if (page.AnswerFourthD.IsFocused)
                    {
                        page.QuestionFourthButton.Content = page.AnswerFourthD.Content;
                        AnswerFourth = "Г";
                    }
                    checkAnswerFourthButton = false;
                }));
            }
        }
        private RelayCommand nextPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ?? (nextPage = new RelayCommand(obj =>
                {
                    if (answerFirst == questionSortList[questionFirst].RightAnswer) resultPoints++;
                    if (answerSecond == questionSortList[questionSecond].RightAnswer) resultPoints++;
                    if (answerThird == questionSortList[questionThird].RightAnswer) resultPoints++;
                    if (answerFourth == questionSortList[questionFourth].RightAnswer) resultPoints++;
                    window.MainFrame.Content = new SecondPageTest(window, resultPoints);
                }));
            }
        }
        #endregion
        #region Property
        public string QuestionFirst { get { return questionSortList[questionFirst].Question; } }
        public string QuestionSecond { get { return questionSortList[questionSecond].Question; } }
        public string QuestionThird { get { return questionSortList[questionThird].Question; } }
        public string QuestionFourth { get { return questionSortList[questionFourth].Question; } }
        public string AnswerFirst
        {
            get { return "Выберите вариант ответа"; }
            set { answerFirst = value; }
        }
        public string AnswerFirstA { get { return questionSortList[questionFirst].AnswerA; } }
        public string AnswerFirstB { get { return questionSortList[questionFirst].AnswerB; } }
        public string AnswerFirstC { get { return questionSortList[questionFirst].AnswerC; } }
        public string AnswerFirstD { get { return questionSortList[questionFirst].AnswerD; } }
        public string AnswerSecond
        {
            get { return "Выберите вариант ответа"; }
            set { answerSecond = value; }
        }
        public string AnswerSecondA { get { return questionSortList[questionSecond].AnswerA; } }
        public string AnswerSecondB { get { return questionSortList[questionSecond].AnswerB; } }
        public string AnswerSecondC { get { return questionSortList[questionSecond].AnswerC; } }
        public string AnswerSecondD { get { return questionSortList[questionSecond].AnswerD; } }
        public string AnswerThird
        {
            get { return "Выберите вариант ответа"; }
            set { answerThird = value; }
        }
        public string AnswerThirdA { get { return questionSortList[questionThird].AnswerA; } }
        public string AnswerThirdB { get { return questionSortList[questionThird].AnswerB; } }
        public string AnswerThirdC { get { return questionSortList[questionThird].AnswerC; } }
        public string AnswerThirdD { get { return questionSortList[questionThird].AnswerD; } }
        public string AnswerFourth
        {
            get { return "Выберите вариант ответа"; }
            set { answerFourth = value; }
        }
        public string AnswerFourthA { get { return questionSortList[questionFourth].AnswerA; } }
        public string AnswerFourthB { get { return questionSortList[questionFourth].AnswerB; } }
        public string AnswerFourthC { get { return questionSortList[questionFourth].AnswerC; } }
        public string AnswerFourthD { get { return questionSortList[questionFourth].AnswerD; } }
        #endregion
        public FirstPageTestViewModel(MainWindow window)
        {
            this.window = window;
            SortList();
            GetRandomNumber();
        }
        private void SortList()
        {
            JsonFileService jsonFile = new JsonFileService();
            questionsOldList = jsonFile.Open(filePath);
            foreach (var item in questionsOldList)
            {
                if (item.TypeQuestion == "Меры безопастности")
                {
                    questionSortList.Add(item);
                }
            }
        }
        private void GetRandomNumber()
        {
            Random random = new Random();
            int[] numb = Enumerable.Range(0, questionSortList.Count).ToArray();

            for (int i = numb.Length - 1; i > 0; i--)
            {
                int j = random.Next(i);
                int t = numb[i];
                numb[i] = numb[j];
                numb[j] = t;
            }
            questionFirst = numb[0];
            questionSecond = numb[1];
            questionThird = numb[2];
            questionFourth = numb[3];
        }
    }
}
