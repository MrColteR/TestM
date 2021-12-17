using System.Windows;
using TestM.ViewModels;

namespace TestM.Views
{
    /// <summary>
    /// Логика взаимодействия для PasswordChanged.xaml
    /// </summary>
    public partial class PasswordChanged : Window
    {
        public PasswordChanged()
        {
            InitializeComponent();
            DataContext = new PasswordChangedViewModel();
        }
    }
}
