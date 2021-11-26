using System.Collections.ObjectModel;
using TestM.ViewModels;

namespace TestM.Data.Base
{
    public interface IFileService
    {
        ObservableCollection<QuestionDataGridViewModel> Open(string filename);
        void Save(string filename, ObservableCollection<QuestionDataGridViewModel> phonesList);
    }
}
