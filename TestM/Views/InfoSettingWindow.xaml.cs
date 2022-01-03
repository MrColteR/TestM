using System.Windows;
using TestM.ViewModels;

namespace TestM.Views
{
    /// <summary>
    /// Логика взаимодействия для InfoSettingWindow.xaml
    /// </summary>
    public partial class InfoSettingWindow : Window
    {
        public InfoSettingWindow()
        {
            InitializeComponent();
            DataContext = new InfoSettingViewModel();
        }
    }
}
