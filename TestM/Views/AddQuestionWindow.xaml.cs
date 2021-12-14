using System.Windows;
using TestM.Data;
using TestM.ViewModels;

namespace TestM.Views
{
    /// <summary>
    /// Логика взаимодействия для AddQuestionWindow.xaml
    /// </summary>
    public partial class AddQuestionWindow : Window
    {
        public AddQuestionWindow(QuestionWindowViewModel data)
        {
            InitializeComponent();
            DataContext = new AddQuestionWindowViewModel(data, new JsonFileService());
        }
    }
}
