using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    public class QuestionDataGridViewModel : ViewModel
    {
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
                OnPropertyChanged(nameof(Question));
            }
        }
        public string TypeQuestion
        {
            get { return typeQuestion; }
            set 
            { 
                typeQuestion = value;
                OnPropertyChanged(nameof(TypeQuestion));
            }
        }
        public string AnswerA
        {
            get { return answerA; }
            set 
            { 
                answerA = value;
                OnPropertyChanged(nameof(AnswerA));
            }
        }
        public string AnswerB
        {
            get { return answerB; }
            set 
            { 
                answerB = value;
                OnPropertyChanged(nameof(AnswerB));
            }
        }
        public string AnswerC
        {
            get { return answerC; }
            set 
            { 
                answerC = value;
                OnPropertyChanged(nameof(AnswerC));
            }
        }
        public string AnswerD
        {
            get { return answerD; }
            set 
            { 
                answerD = value;
                OnPropertyChanged(nameof(AnswerD));
            }
        }
        public string RightAnswer
        {
            get { return rightAnswer; }
            set 
            { 
                rightAnswer = value;
                OnPropertyChanged(nameof(RightAnswer));
            }
        }
        public ObservableCollection<QuestionDataGridViewModel> Data { get; set; }
        //public void BeginningEdit(int index)
        //{
        //    oldTitle = Title;
        //    oldID = index;
        //}
        //public string RowEditEnding(ObservableCollection<QuestionDataGridViewModel> datagrid)
        //{
        //    for (int i = 0; i < datagrid.Count; i++)
        //    {
        //        if (title.ToLower() == datagrid[i].Title.ToLower() && oldID != i)
        //        {
        //            datagrid[oldID].Title = oldTitle;
        //            return "repetition";
        //        }
        //        if (title == "")
        //        {
        //            datagrid[oldID].Title = oldTitle;
        //            return "empty";
        //        }
        //    }
        //    return null;
        //}
        public QuestionDataGridViewModel(ObservableCollection<QuestionDataGridViewModel> Data)
        {
            this.Data = Data;
        }
    }
}
