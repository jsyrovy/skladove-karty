﻿namespace SkladoveKarty.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
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
               new PropertyMetadata(CreateDefaultItem()));

        public static readonly DependencyProperty SelectedStorageCardItemsQtyProperty =
           DependencyProperty.Register(
               nameof(SelectedStorageCardItemsQty),
               typeof(int),
               typeof(MainViewModel),
               new PropertyMetadata(0));

        public static readonly DependencyProperty SelectedStorageCardItemsIncomingPriceProperty =
           DependencyProperty.Register(
               nameof(SelectedStorageCardItemsIncomingPrice),
               typeof(decimal),
               typeof(MainViewModel),
               new PropertyMetadata(0M));

        public static readonly DependencyProperty SelectedStorageCardItemsOutgoingPriceProperty =
           DependencyProperty.Register(
               nameof(SelectedStorageCardItemsOutgoingPrice),
               typeof(decimal),
               typeof(MainViewModel),
               new PropertyMetadata(0M));

        public static readonly DependencyProperty SelectedStorageCardPriceProperty =
           DependencyProperty.Register(
               nameof(SelectedStorageCardItemsPrice),
               typeof(decimal),
               typeof(MainViewModel),
               new PropertyMetadata(0M));

        private StorageCard selectedStorageCard;
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

            this.AddItemCommand = new AddItemCommand(this);
            this.UpdateItemCommand = new UpdateItemCommand(this);
            this.DeleteItemCommand = new DeleteItemCommand(this);
            this.UpdateStorageCardCommand = new UpdateStorageCardCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DatabaseHelper Database { get; private set; } = new();

        public ObservableCollection<Account> Accounts { get; set; } = new();

        public ObservableCollection<Category> Categories { get; set; } = new();

        public ObservableCollection<Customer> Customers { get; set; } = new();

        public ObservableCollection<Item> Items { get; set; } = new();

        public ObservableCollection<StorageCard> StorageCards { get; set; } = new();

        public ObservableCollection<Store> Stores { get; set; } = new();

        public Dictionary<int, string> Movements => new() { [1] = "Příjem", [-1] = "Výdej" };

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
                this.CalculateStorageCardReports();
            }
        }

        public Item NewItem
        {
            get { return (Item)this.GetValue(NewItemProperty); }
            set { this.SetValue(NewItemProperty, value); }
        }

        public decimal SelectedStorageCardItemsIncomingPrice
        {
            get { return (decimal)this.GetValue(SelectedStorageCardItemsIncomingPriceProperty); }
            set { this.SetValue(SelectedStorageCardItemsIncomingPriceProperty, value); }
        }

        public int SelectedStorageCardItemsQty
        {
            get { return (int)this.GetValue(SelectedStorageCardItemsQtyProperty); }
            set { this.SetValue(SelectedStorageCardItemsQtyProperty, value); }
        }

        public decimal SelectedStorageCardItemsOutgoingPrice
        {
            get { return (decimal)this.GetValue(SelectedStorageCardItemsOutgoingPriceProperty); }
            set { this.SetValue(SelectedStorageCardItemsOutgoingPriceProperty, value); }
        }

        public decimal SelectedStorageCardItemsPrice
        {
            get { return (decimal)this.GetValue(SelectedStorageCardPriceProperty); }
            set { this.SetValue(SelectedStorageCardPriceProperty, value); }
        }

        public static Item CreateDefaultItem()
        {
            return new Item() { DateTime = DateTime.Today, Movement = 1, Qty = 1 };
        }

        public void LoadItems()
        {
            this.Items.Clear();

            foreach (var item in this.SelectedStorageCard.Items.OrderBy(i => i.DateTime))
                this.Items.Add(item);
        }

        public void CalculateStorageCardReports()
        {
            this.SelectedStorageCardItemsQty = ReportHelper.GetStorageCardItemsQty(this.SelectedStorageCard);
            this.SelectedStorageCardItemsIncomingPrice = ReportHelper.GetStorageCardItemsPrice(this.SelectedStorageCard, 1);
            this.SelectedStorageCardItemsOutgoingPrice = ReportHelper.GetStorageCardItemsPrice(this.SelectedStorageCard, -1);
            this.SelectedStorageCardItemsPrice = ReportHelper.GetStorageCardItemsPrice(this.SelectedStorageCard);
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
