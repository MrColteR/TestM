using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TestM.Data.Base;
using TestM.Models;

namespace TestM.Data
{
    public class JsonFileService : IFileService
    {
        public ObservableCollection<QuestionModel> Open(string filename)
        {
            ObservableCollection<QuestionModel> items = new ObservableCollection<QuestionModel>() { };
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<QuestionModel>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                items = jsonFormatter.ReadObject(fs) as ObservableCollection<QuestionModel>;
            }
            return items;
        }
        public void Save(string filename, ObservableCollection<QuestionModel> itemsList)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<QuestionModel>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, itemsList);
            }
        }
    }
}
