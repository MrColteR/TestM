using System.Collections.ObjectModel;
using System.IO;
using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    public class PageTestViewModel : ViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileActualQuestion = path.Substring(0, path.IndexOf("bin")) + "ActualQuestion.json";
        private readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";

        private ObservableCollection<QuestionModel> questions;
        private readonly Info actualQuestionModel;
        private readonly JsonFileService service;

        private bool IsAnswered;

        #region Commands
        private RelayCommand firstAnswer;
        public RelayCommand FirstAnswer => firstAnswer ?? (firstAnswer = new RelayCommand(obj => 
        {
            RightAnswer = "А";

            if (!IsAnswered)
            {
                AddingAnsweredQuestions();
                IsAnswered = true;
            }
        }));

        private RelayCommand secondAnswer;
        public RelayCommand SecondAnswer => secondAnswer ?? (secondAnswer = new RelayCommand(obj => 
        {
            RightAnswer = "Б";

            if (!IsAnswered)
            {
                AddingAnsweredQuestions();
                IsAnswered = true;
            }
        }));

        private RelayCommand thirdAnswer;
        public RelayCommand ThirdAnswer => thirdAnswer ?? (thirdAnswer = new RelayCommand(obj =>
        {
            RightAnswer = "В";

            if (!IsAnswered)
            {
                AddingAnsweredQuestions();
                IsAnswered = true;
            }
        }));
        #endregion
        #region Property
        private string rightAnswer;
        public string QuestionFirst => questions[actualQuestionModel.Index].Question;
        public string AnswerFirst => questions[actualQuestionModel.Index].AnswerA;
        public string AnswerSecond => questions[actualQuestionModel.Index].AnswerB;
        public string AnswerThird => questions[actualQuestionModel.Index].AnswerC;
        public string NumberQuestion => $"Вопрос {actualQuestionModel.Index + 1}:";
        public string RightAnswer
        {
            get => rightAnswer;
            set
            {
                rightAnswer = value;
                OnPropertyChanged(nameof(RightAnswer));
            }
        }
        #endregion
        public PageTestViewModel()
        {
            service = new JsonFileService();
            questions = service.Open(fileActualQuestion);
            actualQuestionModel = service.OpenInfo(fileInfo);
            service.SaveIndex(fileInfo);
            IsAnswered = false;
        }
        public void AddingAnsweredQuestions()
        {
            var count = service.OpenCountOfQuestionsAnswered(fileInfo);
            service.SaveCountOfQuestionsAnswered(fileInfo, count + 1);
        }
    }
}
