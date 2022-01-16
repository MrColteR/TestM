using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class AddQuestionWindowViewModel : ViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileData = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        private bool isNull;

        private readonly JsonFileService service;
        private readonly ConvertToString converter;

        #region Commands
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow => closeWindow ?? (closeWindow = new RelayCommand(obj =>
        {
            AddQuestionWindow wnd = obj as AddQuestionWindow;
            wnd.Close();
        }));

        private RelayCommand addQuestion;
        public RelayCommand AddQuestion => addQuestion ?? (addQuestion = new RelayCommand(obj =>
        {
            AddQuestionWindow wnd = obj as AddQuestionWindow;
            isNull = false;

            CheckTextBoxIsNull(wnd.Question);
            CheckTextBoxIsNull(wnd.AnswerATextBox);
            CheckTextBoxIsNull(wnd.AnswerBTextBox);
            CheckTextBoxIsNull(wnd.AnswerCTextBox);
            CheckComboBoxIsNull(wnd.ComboBoxType);
            CheckComboBoxIsNull(wnd.ComboBoxAnswer);
            
            if (isNull)
            {
                EmptyFieldsWindow window = new EmptyFieldsWindow();
                window.Show();
            }
            else
            {
                Data.Add(new QuestionModel()
                {
                    Question = Question,
                    TypeQuestion = (string)converter.Convert(wnd.ComboBoxType.SelectedItem, null, null, null),
                    AnswerA = AnswerA,
                    AnswerB = AnswerB,
                    AnswerC = AnswerC,
                    RightAnswer = (string)converter.Convert(wnd.ComboBoxAnswer.SelectedItem, null, null, null)
                });

                service.Save(fileData, Data);

                ClearTextBox(wnd.Question);
                ClearTextBox(wnd.AnswerATextBox);
                ClearTextBox(wnd.AnswerBTextBox);
                ClearTextBox(wnd.AnswerCTextBox);
                ClearComboBox(wnd.ComboBoxType);
                ClearComboBox(wnd.ComboBoxAnswer);
            }
        }));

        private RelayCommand close;
        public RelayCommand Close => close ?? (close = new RelayCommand(obj =>
        {
            AddQuestionWindow wnd = obj as AddQuestionWindow;
            wnd.Close();
        }));
        #endregion
        #region Property
        public string Question { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public ObservableCollection<QuestionModel> Data { get; set; }
        #endregion
        public AddQuestionWindowViewModel()
        {
            service = new JsonFileService();
            converter = new ConvertToString();

            Data = service.Open(fileData);
        }

        private void CheckTextBoxIsNull(Control obj)
        {
            if (obj.Text == "")
            {
                isNull = true;
            }
        }

        private void CheckComboBoxIsNull(ComboBox obj)
        {
            if (obj.Text == "")
            {
                isNull = true;
            }
        }
        private void ClearTextBox(TextBox obj) => obj.Text = "";
        private void ClearComboBox(ComboBox obj) => obj.Text = "";
    }
}
