using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TestM.Models.Base;

namespace TestM.Models
{
    [DataContract]
    public class Info : Model
    {
        private int index;

        [DataMember]
        public int Index
        {
            get => index;
            set 
            {
                index = value;
                OnPropertyChanged(nameof(Index)); 
            }
        }
        [DataMember]
        private string password;
        public string Password
        {
            get => password; 
            set 
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        [DataMember]
        private int countQuestion;
        public int CountQuestion
        {
            get => countQuestion; 
            set 
            {
                countQuestion = value;
                OnPropertyChanged(nameof(CountQuestion));
            }
        }
        [DataMember]
        private int countQuestionOneType;
        public int CountQuestionOneType
        {
            get => countQuestionOneType; 
            set 
            {
                countQuestionOneType = value;
                OnPropertyChanged(nameof(CountQuestionOneType)); 
            }
        }
        [DataMember]
        private int questionCollection;
        public int QuestionCollection
        {
            get => questionCollection; 
            set
            {
                questionCollection = value; 
                OnPropertyChanged(nameof(QuestionCollection));
            }
        }

    }
}
