using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
        private void btnClose(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
