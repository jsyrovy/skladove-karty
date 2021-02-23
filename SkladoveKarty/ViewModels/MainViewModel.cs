namespace SkladoveKarty.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Threading;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Commands;
    using SkladoveKarty.ViewModels.Helpers;

    public class MainViewModel : DependencyObject, INotifyPropertyChanged
    {
        public static readonly DependencyProperty NewItemProperty =
           DependencyProperty.Register(
               nameof(NewItem),
               typeof(Item),
               typeof(MainViewModel),
               new PropertyMetadata(new Item()));

        private readonly DispatcherTimer newItemTimer;

        private StorageCard selectedStorageCard;
        private string lastActionStatus;
        private DateTime currentDateTime;

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

            this.AddItemCommand = new AddItemCommand(this);
            this.UpdateItemCommand = new UpdateItemCommand(this);
            this.DeleteItemCommand = new DeleteItemCommand(this);
            this.UpdateStorageCardCommand = new UpdateStorageCardCommand(this);

            this.newItemTimer = new();
            this.newItemTimer.Tick += (s, e) => this.CurrentDateTime = DateTime.Now;
            this.newItemTimer.Interval = TimeSpan.FromMilliseconds(200);
            this.newItemTimer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DatabaseHelper Database { get; private set; } = new();

        public ObservableCollection<Account> Accounts { get; set; } = new();

        public ObservableCollection<Category> Categories { get; set; } = new();

        public ObservableCollection<Customer> Customers { get; set; } = new();

        public ObservableCollection<Item> Items { get; set; } = new();

        public ObservableCollection<StorageCard> StorageCards { get; set; } = new();

        public ObservableCollection<Store> Stores { get; set; } = new();

        public UpdateItemCommand UpdateItemCommand { get; set; }

        public AddItemCommand AddItemCommand { get; set; }

        public DeleteItemCommand DeleteItemCommand { get; set; }

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

        public StorageCard SelectedStorageCard
        {
            get
            {
                return this.selectedStorageCard;
            }

            set
            {
                this.selectedStorageCard = value;
                this.OnPropertyChanged(nameof(this.SelectedStorageCard));
                this.LoadItems();
            }
        }

        public DateTime CurrentDateTime
        {
            get
            {
                return this.currentDateTime;
            }

            set
            {
                this.currentDateTime = value;
                this.OnPropertyChanged(nameof(this.CurrentDateTime));
            }
        }

        public Item NewItem
        {
            get { return (Item)this.GetValue(NewItemProperty); }
            set { this.SetValue(NewItemProperty, value); }
        }

        public void LoadItems()
        {
            this.Items.Clear();

            foreach (var item in this.SelectedStorageCard.Items)
                this.Items.Add(item);
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
