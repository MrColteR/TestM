using System.ComponentModel;
using System.Runtime.Serialization;

namespace TestM.Models.Base
{
    [DataContract]
    public abstract class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
