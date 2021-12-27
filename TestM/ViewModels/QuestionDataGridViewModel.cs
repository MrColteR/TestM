using System.Collections.ObjectModel;
using System.IO;
using TestM.Data;
using TestM.Models;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    public class QuestionDataGridViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        private readonly string fileData = path.Substring(0, path.IndexOf("bin")) + "Data.json";

        private QuestionModel model;
        JsonFileService service;

        #region Property
        public string Question
        {
            get => model.Question;
            set
            {
                model.Question = value;
                OnPropertyChanged(nameof(Question));
            }
        }
        public string TypeQuestion
        {
            get => model.TypeQuestion;
            set
            {
                model.TypeQuestion = value;
                OnPropertyChanged(nameof(TypeQuestion));
            }
        }
        public string AnswerA
        {
            get => model.AnswerA;
            set
            {
                model.AnswerA = value;
                OnPropertyChanged(nameof(AnswerA));
            }
        }
        public string AnswerB
        {
            get => model.AnswerB;
            set
            {
                model.AnswerB = value;
                OnPropertyChanged(nameof(AnswerB));
            }
        }
        public string AnswerC
        {
            get => model.AnswerC;
            set
            {
                model.AnswerC = value;
                OnPropertyChanged(nameof(AnswerC));
            }
        }
        public string RightAnswer
        {
            get => model.RightAnswer;
            set
            {
                model.RightAnswer = value;
                OnPropertyChanged(nameof(RightAnswer));
            }
        }
        public ObservableCollection<QuestionModel> Data { get; set; }
        #endregion
        public QuestionDataGridViewModel()
        {
            model = new QuestionModel();
            service = new JsonFileService();
            Data = service.Open(fileData);
        }
    }
}
