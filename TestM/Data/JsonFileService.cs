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
        #region Json Collection QuestionModel
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
        #region Json ActualQuestion
        public Info OpenInfo(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info question = new Info();
            JsonConvert.PopulateObject(json, question);
            if (!File.Exists(filePath))
            {
                question.Index = 0;
                return question;
            }

            return JsonConvert.DeserializeObject<Info>(json);
        }
        public void SaveFirstIndex(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info question = new Info();
            JsonConvert.PopulateObject(json, question);
            question.Index = 0;
            
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(question);
                writer.Write(data);
            }
        }
        public void SaveIndex(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info question = new Info();
            JsonConvert.PopulateObject(json, question);
            if (question.Index != 19)
            {
                question.Index++;
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    var data = JsonConvert.SerializeObject(question);
                    writer.Write(data);
                }
            }
            else
            {
                SaveFirstIndex(filePath);
            }
        }
        #endregion
        #region Json Password
        public string OpenPassword(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            return info.Password;
        }
        public void SavePassword(string filePath, string password)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            info.Password = password;
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(info);
                writer.Write(data);
            }
        }
        #endregion
    }
}
