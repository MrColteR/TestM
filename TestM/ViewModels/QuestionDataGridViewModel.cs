using System.Collections.ObjectModel;
using TestM.Models;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    public class QuestionDataGridViewModel : ViewModel
    {
        private QuestionModel model;
        public string Question
        {
            get { return model.Question; }
            set
            {
                model.Question = value;
                OnPropertyChanged(nameof(Question));
            }
        }
        public string TypeQuestion
        {
            get { return model.TypeQuestion; }
            set
            {
                model.TypeQuestion = value;
                OnPropertyChanged(nameof(TypeQuestion));
            }
        }
        public string AnswerA
        {
            get { return model.AnswerA; }
            set
            {
                model.AnswerA = value;
                OnPropertyChanged(nameof(AnswerA));
            }
        }
        public string AnswerB
        {
            get { return model.AnswerB; }
            set
            {
                model.AnswerB = value;
                OnPropertyChanged(nameof(AnswerB));
            }
        }
        public string AnswerC
        {
            get { return model.AnswerC; }
            set
            {
                model.AnswerC = value;
                OnPropertyChanged(nameof(AnswerC));
            }
        }
        public string AnswerD
        {
            get { return model.AnswerD; }
            set
            {
                model.AnswerD = value;
                OnPropertyChanged(nameof(AnswerD));
            }
        }
        public string RightAnswer
        {
            get { return model.RightAnswer; }
            set
            {
                model.RightAnswer = value;
                OnPropertyChanged(nameof(RightAnswer));
            }
        }
        public ObservableCollection<QuestionModel> Data { get; set; }
        public QuestionDataGridViewModel(ObservableCollection<QuestionModel> Data, QuestionModel model)
        {
            this.model = model;
            this.Data = Data;
        }
    }
}
