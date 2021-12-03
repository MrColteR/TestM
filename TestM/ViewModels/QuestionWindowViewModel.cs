using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.Command;
using TestM.Data;
using TestM.Data.Base;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class QuestionWindowViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        public readonly string fileName = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        IFileService fileService;
        private RelayCommand addQuestion;
        public RelayCommand AddQuestion 
        {
            get 
            {
                return addQuestion ?? (addQuestion = new RelayCommand(obj =>
                {
                    var a = obj as QuestionWindowViewModel;
                    var window = new AddQuestionWindow(obj as QuestionWindowViewModel);
                    window.ShowDialog();
                }));
            }
        }
        private QuestionDataGridViewModel selectedItem;
        private int selectedIndex;
        public QuestionDataGridViewModel SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set 
            { 
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }
        public ObservableCollection<QuestionDataGridViewModel> ItemsSource { get; set; }
        public QuestionWindowViewModel(IFileService fileService)
        {
            this.fileService = fileService;
            ItemsSource = fileService.Open(fileName);
            QuestionDataGridViewModel question = new QuestionDataGridViewModel(ItemsSource);
        }
    }
}
