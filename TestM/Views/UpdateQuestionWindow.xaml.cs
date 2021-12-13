using System.Windows;
using TestM.Models;
using TestM.ViewModels;

namespace TestM.Views
{
    /// <summary>
    /// Логика взаимодействия для UpdateQuestionWindow.xaml
    /// </summary>
    public partial class UpdateQuestionWindow : Window
    {
        public UpdateQuestionWindow(QuestionModel model)
        {
            InitializeComponent();
            DataContext = new UpdateQuestionViewModel(model);
        }
    }
}
