namespace SkladoveKarty.ViewModels
{
    using System.ComponentModel;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    public class BaseViewModel : INotifyPropertyChanged
    {
        private string lastActionStatus;

        public BaseViewModel()
        {
            this.DatabaseHelper = DatabaseHelper.GetInstance(new DatabaseContext());
            this.SettingHelper = SettingHelper.GetInstance(new DatabaseContext());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DatabaseHelper DatabaseHelper { get; private set; }

        public SettingHelper SettingHelper { get; private set; }

        public string LastActionStatus
        {
            get
            {
                return this.lastActionStatus;
            }

            set
            {
                this.lastActionStatus = value;
                this.OnPropertyChanged(nameof(this.LastActionStatus));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
