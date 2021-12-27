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
    public class ActualQuestion : Model
    {
        private int index;

        [DataMember]
        public int Index
        {
            get 
            {
                return index;
            }
            set 
            {
                index = value;
                OnPropertyChanged(nameof(Index)); 
            }
        }
    }
}
