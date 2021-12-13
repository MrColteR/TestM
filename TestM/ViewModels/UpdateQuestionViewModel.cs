using TestM.Command;
using TestM.Models;
using TestM.Enums;
using TestM.Views;
using System.Collections.ObjectModel;

namespace TestM.ViewModels
{
    class UpdateQuestionViewModel
    {
        private bool ChangeTypeQuesion = false;
        private bool ChangeRightAnswer = false;
        private ConvertToString convert = new ConvertToString();
        #region Commands
        private RelayCommand save;
        public RelayCommand Save
        {
            get
            {
                return save ?? (save = new RelayCommand(obj =>
                {
                    if (ChangeTypeQuesion)
                    {
                        SelectedItem.TypeQuestion = (string)convert.Convert(typeQuestion);
                    }

                    if (ChangeRightAnswer)
                    {
                        SelectedItem.RightAnswer = (string)convert.Convert(rightAnswer);
                    }

                    ChangeTypeQuesion = false;
                    ChangeRightAnswer = false;
                    UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
                    wnd.Close();
                }));
            }
        }
        private RelayCommand close;
        public RelayCommand Close
        {
            get
            {
                return close ?? (close = new RelayCommand(obj =>
                {
                    SelectedItem.Question = question;
                    SelectedItem.AnswerA = answerA;
                    SelectedItem.AnswerB = answerB;
                    SelectedItem.AnswerC = answerC;
                    SelectedItem.AnswerD = answerD;
                    UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
                    wnd.Close();
                }));
            }
        }
        #endregion
        #region Property
        private string question;
        private string answerA;
        private string answerB;
        private string answerC;
        private string answerD;
        private TypeQuestions typeQuestion;
        private RightAnswers rightAnswer;
        public string Question
        {
            get { return SelectedItem.Question; }
            set
            {
                SelectedItem.Question = value;
            }
        }
        public TypeQuestions TypeQuestion
        {
            get
            {
                //switch (SelectedItem.TypeQuestion)
                //{
                //    case "Правовые основания":
                //        return TypeQuestions.legalBases;
                //    case "Меры безопастности":
                //        return TypeQuestions.safety;
                //    case "ТТХ":
                //        return TypeQuestions.ttx;
                //    case "Команды":
                //        return TypeQuestions.command;
                //    case "Задержки":
                //        return TypeQuestions.delays;
                //}
                //return typeQuestion;
                return (TypeQuestions)convert.ConvertBack(SelectedItem.TypeQuestion);
            }
            set
            {
                typeQuestion = value;
                ChangeTypeQuesion = true;
            }
        }
        public string AnswerA
        {
            get { return SelectedItem.AnswerA; }
            set
            {
                SelectedItem.AnswerA = value;
            }
        }
        public string AnswerB
        {
            get { return SelectedItem.AnswerB; }
            set
            {
                SelectedItem.AnswerB = value;
            }
        }
        public string AnswerC
        {
            get { return SelectedItem.AnswerC; }
            set
            {
                SelectedItem.AnswerC = value;
            }
        }
        public string AnswerD
        {
            get { return SelectedItem.AnswerD; }
            set
            {
                SelectedItem.AnswerD = value;
            }
        }
        public RightAnswers RightAnswer
        {
            get
            {
                //switch (SelectedItem.RightAnswer)
                //{
                //    case "А":
                //        return RightAnswers.A;
                //    case "Б":
                //        return RightAnswers.B;
                //    case "В":
                //        return RightAnswers.C;
                //    case "Г":
                //        return RightAnswers.D;
                //    default:
                //        break;
                //}
                //return rightAnswer;
                return (RightAnswers)convert.ConvertBack(rightAnswer);
            }
            set
            {
                rightAnswer = value;
                ChangeRightAnswer = true;
            }
        }
        public QuestionModel SelectedItem { get; set; }
        #endregion
        public UpdateQuestionViewModel(QuestionModel selectedItem)
        {
            SelectedItem = selectedItem;
            question = SelectedItem.Question;
            answerA = SelectedItem.AnswerA;
            answerB = SelectedItem.AnswerB;
            answerC = SelectedItem.AnswerC;
            answerD = SelectedItem.AnswerD;
        }
    }
}
