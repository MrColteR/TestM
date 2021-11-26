using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TestM.Data.Base;
using TestM.ViewModels;

namespace TestM.Data
{
    public class JsonFileService : IFileService
    {
        public ObservableCollection<QuestionDataGridViewModel> Open(string filename)
        {
            ObservableCollection<QuestionDataGridViewModel> items = new ObservableCollection<QuestionDataGridViewModel>() { };
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<QuestionDataGridViewModel>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                items = jsonFormatter.ReadObject(fs) as ObservableCollection<QuestionDataGridViewModel>;
            }
            return items;
        }
        public void Save(string filename, ObservableCollection<QuestionDataGridViewModel> itemsList)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<QuestionDataGridViewModel>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, itemsList);
            }
        }
    }
}
