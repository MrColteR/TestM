using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    [DataContract]
    public class QuestionDataGridViewModel : ViewModel
    {
        private string question;
        private string typeQuestion;
        private string answerA;
        private string answerB;
        private string answerC;
        private string answerD;
        private string rightAnswer;
        [DataMember]
        public string Question
        {
            get { return question; }
            set
            {
                question = value;
                OnPropertyChanged(nameof(Question));
            }
        }
        [DataMember]
        public string TypeQuestion
        {
            get { return typeQuestion; }
            set
            {
                typeQuestion = value;
                OnPropertyChanged(nameof(TypeQuestion));
            }
        }
        [DataMember]
        public string AnswerA
        {
            get { return answerA; }
            set
            {
                answerA = value;
                OnPropertyChanged(nameof(AnswerA));
            }
        }
        [DataMember]
        public string AnswerB
        {
            get { return answerB; }
            set
            {
                answerB = value;
                OnPropertyChanged(nameof(AnswerB));
            }
        }
        [DataMember]
        public string AnswerC
        {
            get { return answerC; }
            set
            {
                answerC = value;
                OnPropertyChanged(nameof(AnswerC));
            }
        }
        [DataMember]
        public string AnswerD
        {
            get { return answerD; }
            set
            {
                answerD = value;
                OnPropertyChanged(nameof(AnswerD));
            }
        }
        [DataMember]
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
        public QuestionDataGridViewModel(ObservableCollection<QuestionDataGridViewModel> Data)
        {
            this.Data = Data;
        }
    }
}
