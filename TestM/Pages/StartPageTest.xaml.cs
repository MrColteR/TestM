using System.Windows.Controls;
using TestM.ViewModels;

namespace TestM.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPageTest.xaml
    /// </summary>
    public partial class StartPageTest : Page
    {
        public StartPageTest()
        {
            InitializeComponent();
            DataContext = new StartPageTestViewModel();
        }
    }
}
