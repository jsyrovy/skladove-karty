namespace SkladoveKarty.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Commands;
    using SkladoveKarty.ViewModels.Helpers;

    public class MainViewModel : DependencyObject, INotifyPropertyChanged
    {
        public static readonly DependencyProperty SelectedStorageCardProperty =
           DependencyProperty.Register(
               nameof(SelectedStorageCard),
               typeof(StorageCard),
               typeof(MainViewModel),
               new PropertyMetadata(new StorageCard()));

        private string lastActionStatus;

        public MainViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            foreach (var account in this.Database.GetAccounts())
                this.Accounts.Add(account);

            foreach (var category in this.Database.GetCategories())
                this.Categories.Add(category);

            foreach (var customer in this.Database.GetCustomers())
                this.Customers.Add(customer);

            foreach (var storageCard in this.Database.GetStorageCards())
                this.StorageCards.Add(storageCard);

            foreach (var store in this.Database.GetStores())
                this.Stores.Add(store);

            this.SelectedStorageCard = this.StorageCards.FirstOrDefault();

            this.UpdateItemCommand = new UpdateItemCommand(this);
            this.UpdateStorageCardCommand = new UpdateStorageCardCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DatabaseHelper Database { get; private set; } = new();

        public StorageCard SelectedStorageCard
        {
            get { return (StorageCard)this.GetValue(SelectedStorageCardProperty); }
            set { this.SetValue(SelectedStorageCardProperty, value); }
        }

        public ObservableCollection<Account> Accounts { get; set; } = new();

        public ObservableCollection<Category> Categories { get; set; } = new();

        public ObservableCollection<Customer> Customers { get; set; } = new();

        public ObservableCollection<Item> Items { get; set; } = new();

        public ObservableCollection<StorageCard> StorageCards { get; set; } = new();

        public ObservableCollection<Store> Stores { get; set; } = new();

        public UpdateItemCommand UpdateItemCommand { get; set; }

        public UpdateStorageCardCommand UpdateStorageCardCommand { get; set; }

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

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
