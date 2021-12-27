using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    class StartPageTestViewModel : ViewModel
    {
        #region Property
        private string name;
        private string subdivision;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Subdivision
        {
            get => subdivision;
            set
            {
                subdivision = value;
                OnPropertyChanged(nameof(subdivision));
            }
        }
        #endregion
    }
}
