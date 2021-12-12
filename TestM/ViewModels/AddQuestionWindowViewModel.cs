using System.Collections.ObjectModel;
using System.IO;
using TestM.Command;
using TestM.Data.Base;
using TestM.Models;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class AddQuestionWindowViewModel : ViewModel
    {
        IFileService fileService;
        private static string path = Directory.GetCurrentDirectory();
        public readonly string fileName = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        #region Commands
        private RelayCommand save;
        public RelayCommand Save
        {
            get
            {
                return save ?? (save = new RelayCommand(obj =>
                {
                    ObservableCollection<QuestionModel> models = new ObservableCollection<QuestionModel>() { };
                    QuestionModel model = new QuestionModel();
                    Data.Add(new QuestionModel()
                    {
                        Question = Question,
                        TypeQuestion = TypeQuestion,
                        AnswerA = AnswerA,
                        AnswerB = AnswerB,
                        AnswerC = AnswerC,
                        AnswerD = AnswerD,
                        RightAnswer = RightAnswer
                    });

                    fileService.Save(fileName, Data);
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
                    AddQuestionWindow wnd = obj as AddQuestionWindow;
                    wnd.Close();
                }));
            }
        }
        #endregion
        #region Property
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
            }
        }
        public string TypeQuestion
        {
            get 
            {
                if (typeQuestion == "legalBases")
                {
                    return "Правовые основания";
                }
                else if (typeQuestion == "safety")
                {
                    return "Меры безопастности";
                }
                else if (typeQuestion == "ttx")
                {
                    return "ТТХ";
                }
                else if (typeQuestion == "command")
                {
                    return "Команды";
                }
                else if (typeQuestion == "delays")
                {
                    return "Задержки";
                }
                return typeQuestion; 
            }
            set
            {
                typeQuestion = value;
            }
        }
        public string AnswerA
        {
            get { return answerA; }
            set
            {
                answerA = value;
            }
        }
        public string AnswerB
        {
            get { return answerB; }
            set
            {
                answerB = value;
            }
        }
        public string AnswerC
        {
            get { return answerC; }
            set
            {
                answerC = value;
            }
        }
        public string AnswerD
        {
            get { return answerD; }
            set
            {
                answerD = value;
            }
        }
        public string RightAnswer
        {
            get 
            {
                if (rightAnswer == "A")
                {
                    return "А";
                }
                else if (rightAnswer == "B")
                {
                    return "Б";
                }
                else if (rightAnswer == "C")
                {
                    return "В";
                }
                else if (rightAnswer == "D")
                {
                    return "Г";
                }
                return rightAnswer; 
            }
            set
            {
                rightAnswer = value;
            }
        }
        public ObservableCollection<QuestionModel> Data { get; set; }
        #endregion
        public AddQuestionWindowViewModel(QuestionWindowViewModel data, IFileService fileService)
        {
            this.fileService = fileService;
            Data = data.ItemsSource;
        }
    }
}
