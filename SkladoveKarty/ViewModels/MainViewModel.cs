﻿namespace SkladoveKarty.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Commands;
    using SkladoveKarty.ViewModels.Helpers;

    public class MainViewModel : INotifyPropertyChanged
    {
        private StorageCard selectedStorageCard;
        private Item selectedStorageCardItem;
        private Item newItem;
        private string lastActionStatus;
        private int selectedStorageCardItemsQty;
        private decimal selectedStorageCardItemsIncomingPrice;
        private decimal selectedStorageCardItemsOutgoingPrice;
        private decimal selectedStorageCardItemsPrice;

        public MainViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            this.AddItemCommand = new AddItemCommand(this);
            this.UpdateItemCommand = new UpdateItemCommand(this);
            this.DeleteItemCommand = new DeleteItemCommand(this);
            this.UpdateStorageCardCommand = new UpdateStorageCardCommand(this);
            this.ShowSettingsCommand = new ShowSettingsCommand(this);

            this.NewItem = CreateDefaultItem();

            this.LoadAllAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Dictionary<int, string> Movements => new() { [1] = "Příjem", [-1] = "Výdej" };

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

        public ShowSettingsCommand ShowSettingsCommand { get; set; }

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
                this.LoadItemsAsync();
                this.CalculateStorageCardReports();
            }
        }

        public Item SelectedStorageCardItem
        {
            get
            {
                return this.selectedStorageCardItem;
            }

            set
            {
                this.selectedStorageCardItem = value;
                this.OnPropertyChanged(nameof(this.SelectedStorageCardItem));
            }
        }

        public Item NewItem
        {
            get
            {
                return this.newItem;
            }

            set
            {
                this.newItem = value;
                this.OnPropertyChanged(nameof(this.NewItem));
            }
        }

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

        public int SelectedStorageCardItemsQty
        {
            get
            {
                return this.selectedStorageCardItemsQty;
            }

            set
            {
                this.selectedStorageCardItemsQty = value;
                this.OnPropertyChanged(nameof(this.SelectedStorageCardItemsQty));
            }
        }

        public decimal SelectedStorageCardItemsIncomingPrice
        {
            get
            {
                return this.selectedStorageCardItemsIncomingPrice;
            }

            set
            {
                this.selectedStorageCardItemsIncomingPrice = value;
                this.OnPropertyChanged(nameof(this.SelectedStorageCardItemsIncomingPrice));
            }
        }

        public decimal SelectedStorageCardItemsOutgoingPrice
        {
            get
            {
                return this.selectedStorageCardItemsOutgoingPrice;
            }

            set
            {
                this.selectedStorageCardItemsOutgoingPrice = value;
                this.OnPropertyChanged(nameof(this.SelectedStorageCardItemsOutgoingPrice));
            }
        }

        public decimal SelectedStorageCardItemsPrice
        {
            get
            {
                return this.selectedStorageCardItemsPrice;
            }

            set
            {
                this.selectedStorageCardItemsPrice = value;
                this.OnPropertyChanged(nameof(this.SelectedStorageCardItemsPrice));
            }
        }

        public static Item CreateDefaultItem()
        {
            return new Item() { DateTime = DateTime.Today, Movement = 1, Qty = 1 };
        }

        public async void LoadAllAsync()
        {
            await this.LoadAllTask();
        }

        public async void LoadItemsAsync(Item itemToSelect = null)
        {
            await this.LoadItemsTask(itemToSelect);
        }

        public void CalculateStorageCardReports()
        {
            this.SelectedStorageCardItemsQty = ReportHelper.GetStorageCardItemsQty(this.SelectedStorageCard);
            this.SelectedStorageCardItemsIncomingPrice = ReportHelper.GetStorageCardItemsPrice(this.SelectedStorageCard, 1);
            this.SelectedStorageCardItemsOutgoingPrice = ReportHelper.GetStorageCardItemsPrice(this.SelectedStorageCard, -1);
            this.SelectedStorageCardItemsPrice = ReportHelper.GetStorageCardItemsPrice(this.SelectedStorageCard);
        }

        private Task LoadItemsTask(Item itemToSelect)
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.LoadItems(itemToSelect);
                });
            });
        }

        private Task LoadAllTask()
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Accounts.Clear();

                    foreach (var account in this.Database.GetAccounts())
                        this.Accounts.Add(account);

                    this.Categories.Clear();

                    foreach (var category in this.Database.GetCategories())
                        this.Categories.Add(category);

                    this.Customers.Clear();

                    foreach (var customer in this.Database.GetCustomers())
                        this.Customers.Add(customer);

                    this.StorageCards.Clear();

                    foreach (var storageCard in this.Database.GetStorageCards())
                        this.StorageCards.Add(storageCard);

                    this.Stores.Clear();

                    foreach (var store in this.Database.GetStores())
                        this.Stores.Add(store);
                });
            });
        }

        private void LoadItems(Item itemToSelect)
        {
            this.Items.Clear();

            if (this.SelectedStorageCard == null) return;

            foreach (var item in this.SelectedStorageCard.Items.OrderBy(i => i.DateTime))
                this.Items.Add(item);

            if (itemToSelect != null)
                this.SelectedStorageCardItem = itemToSelect;
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
