using System.Collections.ObjectModel;
using System.IO;
using TestM.Command;
using TestM.Data.Base;
using TestM.Models;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class QuestionWindowViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        public readonly string fileName = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        IFileService fileService;
        QuestionModel questionModel;
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
        private RelayCommand saveFile;
        public RelayCommand SaveFile
        {
            get
            {
                return saveFile ?? (saveFile = new RelayCommand(obj =>
                {
                    ObservableCollection<QuestionModel> data = obj as ObservableCollection<QuestionModel>;
                    fileService.Save(fileName, data);
                }));
            }
        }
        private RelayCommand closeQuestionWindow;
        public RelayCommand CloseQuestionWindow
        {
            get
            {
                return closeQuestionWindow ?? (closeQuestionWindow = new RelayCommand(obj =>
                {
                    QuestionWindow wnd = obj as QuestionWindow;
                    wnd.Close();
                }));
            }
        }
        private QuestionModel selectedItem;
        private int selectedIndex;
        public QuestionModel SelectedItem
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
        public ObservableCollection<QuestionModel> ItemsSource { get; set; }
        public QuestionWindowViewModel(IFileService fileService)
        {
            this.fileService = fileService;
            ItemsSource = fileService.Open(fileName);
            QuestionDataGridViewModel question = new QuestionDataGridViewModel(ItemsSource, questionModel);
        }
    }
}
