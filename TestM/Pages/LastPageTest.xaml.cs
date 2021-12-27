using System.Windows.Controls;
using TestM.ViewModels;

namespace TestM.Pages
{
    /// <summary>
    /// Логика взаимодействия для LastPageTest.xaml
    /// </summary>
    public partial class LastPageTest : Page
    {
        public LastPageTest()
        {
            InitializeComponent();
            DataContext = new LastPageTestViewModel();
        }
    }
}
