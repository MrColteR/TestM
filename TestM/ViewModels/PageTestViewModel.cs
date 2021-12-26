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
    public class PageTestViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        private readonly string fileActualQuestion = path.Substring(0, path.IndexOf("bin")) + "ActualQuestion.json";
        private readonly string fileIndex = path.Substring(0, path.IndexOf("bin")) + "Index.json";

        private ObservableCollection<QuestionModel> question;
        private ActualQuestion index;

        //private string rightAnswer;
        //private int questionFirst;
        #region Command
        private RelayCommand firstAnswer;
        public RelayCommand FirstAnswer
        {
            get
            {
                return firstAnswer ?? (firstAnswer = new RelayCommand(obj => 
                {
                    RightAnswer = "А";
                }));
            }
        }
        private RelayCommand secondAnswer;
        public RelayCommand SecondAnswer
        {
            get
            {
                return secondAnswer ?? (secondAnswer = new RelayCommand(obj => 
                {
                    RightAnswer = "Б";
                }));
            }
        }
        private RelayCommand thirdAnswer;
        public RelayCommand ThirdAnswer
        {
            get
            {
                return thirdAnswer ?? (thirdAnswer = new RelayCommand(obj =>
                {
                    RightAnswer = "В";
                }));
            }
        }
        #endregion
        #region Property
        public string QuestionFirst { get { return question[index.Index].Question; } }
        private string answerFirst;
        public string AnswerFirst
        {
            get
            {
                return question[index.Index].AnswerA;
            }
            set
            {
                answerFirst = value; 
            }
        }
        private string answerSecond;
        public string AnswerSecond 
        {
            get
            {
                return question[index.Index].AnswerB;
            }
            set
            {
                answerSecond = value;
            }
        }
        private string answerThird;
        public string AnswerThird 
        {
            get
            {
                return question[index.Index].AnswerC;
            }
            set
            {
                answerThird = value;
            }
        }
        private string rightAnswer;
        public string RightAnswer
        {
            get 
            {
                return rightAnswer; 
            }
            set
            {
                rightAnswer = value;
                OnPropertyChanged(nameof(RightAnswer));
            }
        }
        #endregion
        public PageTestViewModel()
        {
            JsonFileService service = new JsonFileService();
            question = service.Open(fileActualQuestion);
            index = service.OpenIndex(fileIndex);
            service.SaveIndex(fileIndex);
        }
    }
}
