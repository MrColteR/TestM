using System.Windows.Controls;
using TestM.ViewModels;

namespace TestM.Pages
{
    /// <summary>
    /// Логика взаимодействия для SecondPageTest.xaml
    /// </summary>
    public partial class SecondPageTest : Page
    {
        public SecondPageTest(MainWindow window, int resultPoints)
        {
            InitializeComponent();
            DataContext = new SecondPageTestViewModel(window, resultPoints);
        }
    }
}
