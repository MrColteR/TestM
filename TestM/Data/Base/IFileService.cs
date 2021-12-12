using System.Collections.ObjectModel;
using TestM.Models;

namespace TestM.Data.Base
{
    public interface IFileService
    {
        ObservableCollection<QuestionModel> Open(string filename);
        void Save(string filename, ObservableCollection<QuestionModel> phonesList);
    }
}
