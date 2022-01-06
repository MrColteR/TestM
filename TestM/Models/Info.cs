using System.Collections.Generic;
using System.Runtime.Serialization;
using TestM.Models.Base;

namespace TestM.Models
{
    [DataContract]
    public class Info : Model
    {
        private bool saveFileCheck;
        [DataMember]
        public bool SaveFileCheck
        {
            get => saveFileCheck;
            set
            {
                saveFileCheck = value;
                OnPropertyChanged(nameof(SaveFileCheck));
            }
        }
        private bool stopTestCheck;
        [DataMember]
        public bool StopTestCheck
        {
            get => stopTestCheck;
            set
            {
                stopTestCheck = value;
                OnPropertyChanged(nameof(StopTestCheck));
            }
        }
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
        private string password;
        [DataMember]
        public string Password
        {
            get => password; 
            set 
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private int countQuestion;
        [DataMember]
        public int CountQuestion
        {
            get => countQuestion; 
            set 
            {
                countQuestion = value;
                OnPropertyChanged(nameof(CountQuestion));
            }
        }
        private int countQuestionOneType;
        [DataMember]
        public int CountQuestionOneType
        {
            get => countQuestionOneType; 
            set 
            {
                countQuestionOneType = value;
                OnPropertyChanged(nameof(CountQuestionOneType)); 
            }
        }
        private string minimalCountPoints;
        [DataMember]
        public string MinimalCountPoints
        {
            get => minimalCountPoints;
            set
            {
                minimalCountPoints = value;
                OnPropertyChanged(nameof(MinimalCountPoints));
            }
        }
        private Dictionary<int, bool> buttonsStates;
        [DataMember]
        public Dictionary<int, bool> ButtonsStates
        {
            get => buttonsStates; 
            set 
            {
                buttonsStates = value;
                OnPropertyChanged(nameof(ButtonsStates));
            }
        }
    }
}
