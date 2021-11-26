using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestM.Models
{
    public class QuestionModel
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
            set { question = value; }
        }
        public string TypeQuestion
        {
            get { return typeQuestion; }
            set { typeQuestion = value; }
        }
        public string AnswerA
        {
            get { return answerA; }
            set { answerA = value; }
        }
        public string AnswerB
        {
            get { return answerB; }
            set { answerB = value; }
        }
        public string AnswerC
        {
            get { return answerC; }
            set { answerC = value; }
        }
        public string AnswerD
        {
            get { return answerD; }
            set { answerD = value; }
        }
        public string RightAnswer
        {
            get { return rightAnswer; }
            set { rightAnswer = value; }
        }
    }
}
