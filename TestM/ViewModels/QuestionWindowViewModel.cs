using System.Collections.ObjectModel;
using System.IO;
using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class QuestionWindowViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        public readonly string fileName = path.Substring(0, path.IndexOf("bin")) + "Data.json";

        JsonFileService fileService;
        QuestionDataGridViewModel question;

        private bool checkWindowState = false;

        #region Commands
        private RelayCommand minimizeWindow;
        public RelayCommand MinimizeWindow
        {
            get
            {
                return minimizeWindow ?? (minimizeWindow = new RelayCommand(obj =>
                {
                    QuestionWindow wnd = obj as QuestionWindow;
                    wnd.WindowState = System.Windows.WindowState.Minimized;
                }));
            }
        }
        private RelayCommand windowStateButton;
        public RelayCommand WindowStateButton
        {
            get
            {
                return windowStateButton ?? (windowStateButton = new RelayCommand(obj =>
                {
                    QuestionWindow wnd = obj as QuestionWindow;
                    if (checkWindowState)
                    {
                        wnd.WindowState = System.Windows.WindowState.Normal;
                        checkWindowState = false;
                    }
                    else if (!checkWindowState)
                    {
                        wnd.WindowState = System.Windows.WindowState.Maximized;
                        checkWindowState = true;
                    }
                }));
            }
        }
        private RelayCommand closeWindow;
        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow ?? (closeWindow = new RelayCommand(obj =>
                {
                    QuestionWindow wnd = obj as QuestionWindow;
                    wnd.Close();
                }));
            }
        }
        private RelayCommand addQuestion;
        public RelayCommand AddQuestion
        {
            get
            {
                return addQuestion ?? (addQuestion = new RelayCommand(obj =>
                {
                    AddQuestionWindow window = new AddQuestionWindow(obj as QuestionWindowViewModel);
                    window.ShowDialog();
                }));
            }
        }
        private RelayCommand updateQuestion;
        public RelayCommand UpdateQuestion 
        {
            get
            {
                return updateQuestion ?? (updateQuestion = new RelayCommand(obj => 
                {
                    UpdateQuestionWindow window = new UpdateQuestionWindow(SelectedItem);
                    window.ShowDialog();
                }));
            }
        }
        private RelayCommand deleteQustion;
        public RelayCommand DeleteQuestion 
        {
            get
            {
                return deleteQustion ?? (deleteQustion = new RelayCommand(obj =>
                {
                    var index = ItemsSource.IndexOf(SelectedItem);
                    if (SelectedItem != null)
                    {
                        ItemsSource.Remove(SelectedItem);
                        if (index == ItemsSource.Count)
                        {
                            SelectedIndex = index - 1;
                        }
                        else
                        {
                            SelectedIndex = index;
                        }
                    }
                }, (obj) => ItemsSource.Count > 0));
            }
        }
        private RelayCommand saveFile;
        public RelayCommand SaveFile
        {
            get
            {
                return saveFile ?? (saveFile = new RelayCommand(obj =>
                {
                    QuestionWindow wnd = obj as QuestionWindow;
                    fileService.Save(fileName, ItemsSource);
                    wnd.Close();
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
        #endregion
        #region Property
        private QuestionModel selectedItem;
        private int selectedIndex;
        private ObservableCollection<QuestionModel> itemsSource;
        public QuestionModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }
        public ObservableCollection<QuestionModel> ItemsSource
        {
            get => itemsSource;
            set 
            {
                itemsSource = value;
                OnPropertyChanged(nameof(ItemsSource));
            }
        }
        #endregion
        public QuestionWindowViewModel()
        {
            question = new QuestionDataGridViewModel();
            fileService = new JsonFileService();
            ItemsSource = fileService.Open(fileName);
        }
    }
}
