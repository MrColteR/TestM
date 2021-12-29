using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TestM.Models;

namespace TestM.Data
{
    public class JsonFileService
    {
        #region JsonCollection QuestionModel
        public ObservableCollection<QuestionModel> Open(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new ObservableCollection<QuestionModel>();
            }
            using (StreamReader reader = File.OpenText(filePath))
            {
                var data = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ObservableCollection<QuestionModel>>(data);
            }
        }
        public void Save(string filePath, ObservableCollection<QuestionModel> itemsList)
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(itemsList);
                writer.Write(data);
            }
        }
        #endregion
        #region JsonActualQuestion
        public ActualQuestion OpenIndex(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new ActualQuestion();
            }
            using (StreamReader reader = File.OpenText(filePath))
            {
                var data = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ActualQuestion>(data);
            }
        }
        public void SaveIndexFirst(string filePath, ActualQuestion item)
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(item);
                writer.Write(data);
            }
        }
        public void SaveIndex(string filePath)
        {
            ActualQuestion numberInFile = OpenIndex(filePath);
            if (numberInFile.Index != 19)
            {
                numberInFile.Index++;
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    var data = JsonConvert.SerializeObject(numberInFile);
                    writer.Write(data);
                }
            }
            else
            {
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    var data = JsonConvert.SerializeObject(new ActualQuestion());
                    writer.Write(data);
                }
            }
        }
        #endregion
    }
}
