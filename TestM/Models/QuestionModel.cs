using System.Runtime.Serialization;
using TestM.Models.Base;

namespace TestM.Models
{
    [DataContract]
    public class QuestionModel : Model
    {
        private string question;
        private string typeQuestion;
        private string answerA;
        private string answerB;
        private string answerC;
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
        public string RightAnswer
        {
            get { return rightAnswer; }
            set
            {
                rightAnswer = value;
                OnPropertyChanged(nameof(RightAnswer)); 
            }
        }
    }
}
