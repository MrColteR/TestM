using System.Windows.Controls;
using TestM.ViewModels;

namespace TestM.Pages
{
    /// <summary>
    /// Логика взаимодействия для FirstPageTest.xaml
    /// </summary>
    public partial class FirstPageTest : Page
    {
        public FirstPageTest(MainWindow window)
        {
            InitializeComponent();
            DataContext = new FirstPageTestViewModel(window);
        }
    }
}
