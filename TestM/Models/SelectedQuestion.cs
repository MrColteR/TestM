using System.Runtime.Serialization;
using TestM.ViewModels.Base;

namespace TestM.Models
{
    public class SelectedQuestion : ViewModel
    {
        private string question;
        private string typeQuestion;
        private string answerA;
        private string answerB;
        private string answerC;
        private string rightAnswer;
        private int index;
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
        public string RightAnswer
        {
            get { return rightAnswer; }
            set
            {
                rightAnswer = value;
                OnPropertyChanged(nameof(RightAnswer));
            }
        }
        [DataMember]
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged(nameof(Index));
            }
        }
    }
}
