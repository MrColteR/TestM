using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            bool isNull = false;

            if (wnd.Question.Text == "")
            {
                isNull = true;
                wnd.Question.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.Question.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (wnd.AnswerATextBox.Text == "")
            {
                isNull = true;
                wnd.AnswerATextBox.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.AnswerATextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (wnd.AnswerBTextBox.Text == "")
            {
                isNull = true;
                wnd.AnswerBTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.AnswerBTextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (wnd.AnswerCTextBox.Text == "")
            {
                isNull = true;
                wnd.AnswerCTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.AnswerCTextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (wnd.ComboType.Text is null)
            {
                isNull = true;
                wnd.ComboType.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.ComboType.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (wnd.ComboBoxAnswer.Text is null)
            {
                isNull = true;
                wnd.ComboBoxAnswer.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            }
            else
                wnd.ComboBoxAnswer.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

            if (!isNull)
            {
                Data.Add(new QuestionModel()
                {
                    Question = Question,
                    TypeQuestion = (string)converter.Convert(wnd.ComboType.SelectedItem, null, null, null),
                    AnswerA = AnswerA,
                    AnswerB = AnswerB,
                    AnswerC = AnswerC,
                    RightAnswer = (string)converter.Convert(wnd.ComboBoxAnswer.SelectedItem, null, null, null)
                });

                wnd.Question.Clear();
                wnd.Question.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

                wnd.AnswerATextBox.Clear();
                wnd.AnswerATextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

                wnd.AnswerBTextBox.Clear();
                wnd.AnswerBTextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

                wnd.AnswerCTextBox.Clear();
                wnd.AnswerCTextBox.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

                wnd.ComboType.Text = "";
                wnd.ComboType.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));

                wnd.ComboBoxAnswer.Text = "";
                wnd.ComboBoxAnswer.Background = new SolidColorBrush(Color.FromRgb(147, 204, 234));
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
        public AddQuestionWindowViewModel(QuestionWindowViewModel data)
        {
            Data = data.ItemsSource;
            converter = new ConvertToString();
        }
    }
}
