using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class UpdateQuestionViewModel : ViewModel
    {
        private readonly ConvertToString converter;

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
                SelectedItem.TypeQuestion = (string)converter.Convert(ComboBoxTypeText, null, null, null);
                SelectedItem.RightAnswer = (string)converter.Convert(ComboBoxRightAnswerText, null, null, null);

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
            SelectedItem.Question = question;
            SelectedItem.AnswerA = answerA;
            SelectedItem.AnswerB = answerB;
            SelectedItem.AnswerC = answerC;

            UpdateQuestionWindow wnd = obj as UpdateQuestionWindow;
            wnd.Close();
        }));

        #endregion
        #region Property
        private string question;
        private string answerA;
        private string answerB;
        private string answerC;
        private TypeEnum comboBoxTypeText;
        private AnswerEnum comboBoxRightAnswerText;
        public string Question
        {
            get => SelectedItem.Question;
            set => SelectedItem.Question = value;
        }
        public TypeEnum ComboBoxTypeText
        {
            get => comboBoxTypeText;
            set => comboBoxTypeText = value;
        }
        public string AnswerA
        {
            get => SelectedItem.AnswerA;
            set => SelectedItem.AnswerA = value;
        }
        public string AnswerB
        {
            get => SelectedItem.AnswerB;
            set => SelectedItem.AnswerB = value;
        }
        public string AnswerC
        {
            get => SelectedItem.AnswerC;
            set => SelectedItem.AnswerC = value;
        }
        public AnswerEnum ComboBoxRightAnswerText
        {
            get => comboBoxRightAnswerText;
            set => comboBoxRightAnswerText = value;
        }
        public QuestionModel SelectedItem { get; set; }
        #endregion
        public UpdateQuestionViewModel(QuestionModel selectedItem)
        {
            converter = new ConvertToString();
            SelectedItem = selectedItem;
            question = SelectedItem.Question;
            answerA = SelectedItem.AnswerA;
            answerB = SelectedItem.AnswerB;
            answerC = SelectedItem.AnswerC;
            comboBoxTypeText = (TypeEnum)converter.ConvertBack(SelectedItem.TypeQuestion, null, null, null);
            comboBoxRightAnswerText = (AnswerEnum)converter.ConvertBack(SelectedItem.RightAnswer, null, null, null);
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
