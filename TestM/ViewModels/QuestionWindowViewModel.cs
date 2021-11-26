using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestM.Data.Base;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    public class QuestionWindowViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        public readonly string fileName = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        IFileService fileService;

        private int itemSourse;
        private int selectedItem;
        private int selectedIndex;
        private List<TypeQuestion> typeQuestions = new List<TypeQuestion>()
        {
            TypeQuestion.legalBases,
            TypeQuestion.safety,
            TypeQuestion.ttx,
            TypeQuestion.command,
            TypeQuestion.delays
        };
        public int ItemSourse
        {
            get { return itemSourse; }
            set 
            { 
                itemSourse = value;
                OnPropertyChanged(nameof(ItemSourse));
            }
        }
        public int SelectedItem
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
        public List<TypeQuestion> TypeQuestions
        {
            get { return typeQuestions; }
            set 
            { 
                typeQuestions = value;
                OnPropertyChanged(nameof(TypeQuestions));
            }
        }
        public ObservableCollection<QuestionDataGridViewModel> Data { get; set; }
        public QuestionWindowViewModel(IFileService fileService)
        {
            this.fileService = fileService;
            Data = fileService.Open(fileName);
            QuestionDataGridViewModel question = new QuestionDataGridViewModel(Data);
        }
    }
}
