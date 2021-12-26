using System.Windows.Controls;
using TestM.ViewModels;

namespace TestM.Pages
{
    /// <summary>
    /// Логика взаимодействия для FirstPageTest.xaml
    /// </summary>
    public partial class PageTest : Page
    {
        public PageTest()
        {
            InitializeComponent();
            DataContext = new PageTestViewModel();
        }
    }
}
