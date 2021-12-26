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
        
        public ActualQuestion OpenIndex(string filename)
        {
            ActualQuestion item = new ActualQuestion();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ActualQuestion));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                item = jsonFormatter.ReadObject(fs) as ActualQuestion;
            }
            return item;
        }

        public void SaveIndexFirst(string filename, ActualQuestion item)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ActualQuestion));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, item);
            }
        }
        public void SaveIndex(string filename)
        {
            ActualQuestion numberInFile = OpenIndex(filename);
            if (numberInFile.Index != 19)
            {
                numberInFile.Index++;
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ActualQuestion));
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, numberInFile);
                }
            }
            else 
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ActualQuestion));
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, 0);
                }
            }
        }
    }
}
