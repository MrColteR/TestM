using System.Collections.ObjectModel;
using System.IO;
using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class UpdateQuestionViewModel : ViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileNewData = path.Substring(0, path.IndexOf("bin")) + "NewData.json";
        private readonly string fileSelectedQuestion = path.Substring(0, path.IndexOf("bin")) + "SelectedQuestion.json";

        private readonly JsonFileService service;
        private readonly ConvertToString converter;
        private SelectedQuestion selectedQuestion;

        private bool isNull;

        #region Commands
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow => closeWindow ?? (closeWindow = new RelayCommand(obj =>
        {
            UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
            wnd.Close();
        }));

        private RelayCommand save;
        public RelayCommand Save => save ?? (save = new RelayCommand(obj =>
        {
            isNull = false;
            CheckTextBoxIsNull(Question);
            CheckTextBoxIsNull(AnswerA);
            CheckTextBoxIsNull(AnswerB);
            CheckTextBoxIsNull(AnswerC);

            if (!isNull)
            {
                for (int i = 0; i < OldQuestions.Count; i++)
                {
                    if (selectedQuestion.Index == i)
                    {
                        OldQuestions[i] = new QuestionModel()
                        {
                            Question = Question,
                            TypeQuestion = (string)converter.Convert(ComboBoxTypeText, null, null, null),
                            AnswerA = AnswerA,
                            AnswerB = AnswerB,
                            AnswerC = AnswerC,
                            RightAnswer = (string)converter.Convert(ComboBoxRightAnswerText, null, null, null)
                        };
                    }
                }
                service.Save(fileNewData, OldQuestions);

                UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
                wnd.Close();
            }
            else
            {
                EmptyFieldsWindow window = new EmptyFieldsWindow();
                window.Show();
            }
        }));

        private RelayCommand close;
        public RelayCommand Close => close ?? (close = new RelayCommand(obj =>
        {
            UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
            wnd.Close();
        }));
        #endregion
        #region Property
        public string Question { get; set; }
        public TypeEnum ComboBoxTypeText { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public AnswerEnum ComboBoxRightAnswerText { get; set; }
        public QuestionModel SelectedItem { get; set; }
        public ObservableCollection<QuestionModel> OldQuestions { get; set; }
        #endregion
        public UpdateQuestionViewModel()
        {
            service = new JsonFileService();
            converter = new ConvertToString();
            selectedQuestion = new SelectedQuestion();

            OldQuestions = service.Open(fileNewData);
            selectedQuestion = service.OpenSelectedQuestion(fileSelectedQuestion);

            Question = selectedQuestion.Question;
            AnswerA = selectedQuestion.AnswerA;
            AnswerB = selectedQuestion.AnswerB;
            AnswerC = selectedQuestion.AnswerC;
            ComboBoxTypeText = (TypeEnum)converter.ConvertBack(selectedQuestion.TypeQuestion, null, null, null);
            ComboBoxRightAnswerText = (AnswerEnum)converter.ConvertBack(selectedQuestion.RightAnswer, null, null, null);
        }

        private void CheckTextBoxIsNull(string value)
        {
            if (value == "")
            {
                isNull = true;
            }
        }
    }
}
