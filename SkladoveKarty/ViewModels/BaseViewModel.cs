﻿namespace SkladoveKarty.ViewModels
{
    using System.ComponentModel;
    using SkladoveKarty.ViewModels.Helpers;

    public class BaseViewModel : INotifyPropertyChanged
    {
        private string lastActionStatus;

        public event PropertyChangedEventHandler PropertyChanged;

        public DatabaseHelper Database { get; private set; } = new();

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
