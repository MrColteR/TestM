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
        #region Json Info Setting
        public string OpenStyleApp(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            return info.StyleApp;
        }
        public bool OpenSaveFileCheck(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            return info.SaveFileCheck;
        }
        public bool OpenStopCheckCheck(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            return info.StopTestCheck;
        }
        public int OpenCountQuestionOneType(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            return info.CountQuestionOneType;
        }
        public int OpenCountQuestion(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            return info.CountQuestion;
        }
        public int OpenCountOfQuestionsAnswered(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            return info.CountOfQuestionsAnswered;
        }
        public string OpenMinimalCountPonits(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            return info.MinimalCountPoints;
        }
        public Dictionary<int, bool> OpenButtonStates(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            return info.ButtonsStates;
        }
        public void SaveStyleApp(string filePath, string style)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            info.StyleApp = style;
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(info);
                writer.Write(data);
            }
        }
        public void SaveQuestionsInfo(string filePath, int countQuestionOneType, int countQuestion)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            info.CountQuestionOneType = countQuestionOneType;
            info.CountQuestion = countQuestion;
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(info);
                writer.Write(data);
            }
        }
        public void SaveCountOfQuestionsAnswered(string filePath, int count)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            info.CountOfQuestionsAnswered = count;
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(info);
                writer.Write(data);
            }
        }
        public void SaveButtonsStates(string filePath, Dictionary<int, bool> buttonsStates, string minimalCountPonits)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            info.ButtonsStates = buttonsStates;
            info.MinimalCountPoints = minimalCountPonits;
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(info);
                writer.Write(data);
            }
        }
        public void SaveFileCheck(string filePath, bool check)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            info.SaveFileCheck = check;
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(info);
                writer.Write(data);
            }
        }
        public void SaveStopTestCheck(string filePath, bool check)
        {
            string json = File.ReadAllText(filePath);
            Info info = new Info();
            JsonConvert.PopulateObject(json, info);
            info.StopTestCheck = check;
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(info);
                writer.Write(data);
            }
        }
        #endregion
    }
}
