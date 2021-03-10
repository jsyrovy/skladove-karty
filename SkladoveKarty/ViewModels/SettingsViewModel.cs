﻿namespace SkladoveKarty.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Commands;
    using SkladoveKarty.ViewModels.Helpers;

    public class SettingsViewModel : INotifyPropertyChanged
    {
        private string lastActionStatus;
        private Account newAccount;
        private Category newCategory;
        private Customer newCustomer;
        private Store newStore;
        private Supplier newSupplier;
        private Account selectedAccount;
        private Category selectedCategory;
        private Customer selectedCustomer;
        private Store selectedStore;
        private Supplier selectedSupplier;

        public SettingsViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            this.AddAccountCommand = new AddAccountCommand(this);
            this.AddCategoryCommand = new AddCategoryCommand(this);
            this.AddCustomerCommand = new AddCustomerCommand(this);
            this.AddStoreCommand = new AddStoreCommand(this);
            this.AddSupplierCommand = new AddSupplierCommand(this);

            this.UpdateAccountCommand = new UpdateAccountCommand(this);
            this.UpdateCategoryCommand = new UpdateCategoryCommand(this);
            this.UpdateCustomerCommand = new UpdateCustomerCommand(this);
            this.UpdateStoreCommand = new UpdateStoreCommand(this);
            this.UpdateSupplierCommand = new UpdateSupplierCommand(this);

            this.DeleteAccountCommand = new DeleteAccountCommand(this);
            this.DeleteCategoryCommand = new DeleteCategoryCommand(this);
            this.DeleteCustomerCommand = new DeleteCustomerCommand(this);
            this.DeleteStoreCommand = new DeleteStoreCommand(this);
            this.DeleteSupplierCommand = new DeleteSupplierCommand(this);

            this.NewAccount = new();
            this.NewCategory = new();
            this.NewCustomer = new();
            this.NewStore = new();
            this.NewSupplier = new();

            this.LoadAccountsAsync();
            this.LoadCategoriesAsync();
            this.LoadCustomersAsync();
            this.LoadStoresAsync();
            this.LoadSuppliersAsync();
        }

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

        public Account NewAccount
        {
            get
            {
                return this.newAccount;
            }

            set
            {
                this.newAccount = value;
                this.OnPropertyChanged(nameof(this.NewAccount));
            }
        }

        public Category NewCategory
        {
            get
            {
                return this.newCategory;
            }

            set
            {
                this.newCategory = value;
                this.OnPropertyChanged(nameof(this.NewCategory));
            }
        }

        public Customer NewCustomer
        {
            get
            {
                return this.newCustomer;
            }

            set
            {
                this.newCustomer = value;
                this.OnPropertyChanged(nameof(this.NewCustomer));
            }
        }

        public Store NewStore
        {
            get
            {
                return this.newStore;
            }

            set
            {
                this.newStore = value;
                this.OnPropertyChanged(nameof(this.NewStore));
            }
        }

        public Supplier NewSupplier
        {
            get
            {
                return this.newSupplier;
            }

            set
            {
                this.newSupplier = value;
                this.OnPropertyChanged(nameof(this.NewSupplier));
            }
        }

        public Account SelectedAccount
        {
            get
            {
                return this.selectedAccount;
            }

            set
            {
                this.selectedAccount = value;
                this.OnPropertyChanged(nameof(this.SelectedAccount));
            }
        }

        public Category SelectedCategory
        {
            get
            {
                return this.selectedCategory;
            }

            set
            {
                this.selectedCategory = value;
                this.OnPropertyChanged(nameof(this.SelectedCategory));
            }
        }

        public Customer SelectedCustomer
        {
            get
            {
                return this.selectedCustomer;
            }

            set
            {
                this.selectedCustomer = value;
                this.OnPropertyChanged(nameof(this.SelectedCustomer));
            }
        }

        public Store SelectedStore
        {
            get
            {
                return this.selectedStore;
            }

            set
            {
                this.selectedStore = value;
                this.OnPropertyChanged(nameof(this.SelectedStore));
            }
        }

        public Supplier SelectedSupplier
        {
            get
            {
                return this.selectedSupplier;
            }

            set
            {
                this.selectedSupplier = value;
                this.OnPropertyChanged(nameof(this.SelectedSupplier));
            }
        }

        public ObservableCollection<Category> Categories { get; set; } = new();

        public ObservableCollection<Store> Stores { get; set; } = new();

        public ObservableCollection<Account> Accounts { get; set; } = new();

        public ObservableCollection<Customer> Customers { get; set; } = new();

        public ObservableCollection<Supplier> Suppliers { get; set; } = new();

        public AddAccountCommand AddAccountCommand { get; set; }

        public AddCategoryCommand AddCategoryCommand { get; set; }

        public AddCustomerCommand AddCustomerCommand { get; set; }

        public AddStoreCommand AddStoreCommand { get; set; }

        public AddSupplierCommand AddSupplierCommand { get; set; }

        public UpdateAccountCommand UpdateAccountCommand { get; set; }

        public UpdateCategoryCommand UpdateCategoryCommand { get; set; }

        public UpdateCustomerCommand UpdateCustomerCommand { get; set; }

        public UpdateStoreCommand UpdateStoreCommand { get; set; }

        public UpdateSupplierCommand UpdateSupplierCommand { get; set; }

        public DeleteAccountCommand DeleteAccountCommand { get; set; }

        public DeleteCategoryCommand DeleteCategoryCommand { get; set; }

        public DeleteCustomerCommand DeleteCustomerCommand { get; set; }

        public DeleteStoreCommand DeleteStoreCommand { get; set; }

        public DeleteSupplierCommand DeleteSupplierCommand { get; set; }

        public async void LoadAccountsAsync(Account accountToSelect = null)
        {
            await this.LoadAccountsTask(accountToSelect);
        }

        public async void LoadCategoriesAsync(Category categoryToSelect = null)
        {
            await this.LoadCategoriesTask(categoryToSelect);
        }

        public async void LoadCustomersAsync(Customer customerToSelect = null)
        {
            await this.LoadCustomersTask(customerToSelect);
        }

        public async void LoadStoresAsync(Store storeToSelect = null)
        {
            await this.LoadStoresTask(storeToSelect);
        }

        public async void LoadSuppliersAsync(Supplier supplierToSelect = null)
        {
            await this.LoadSuppliersTask(supplierToSelect);
        }

        private Task LoadAccountsTask(Account accountToSelect)
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Accounts.Clear();

                    foreach (var account in this.Database.GetAccounts())
                        this.Accounts.Add(account);

                    if (accountToSelect != null)
                        this.SelectedAccount = accountToSelect;
                });
            });
        }

        private Task LoadCategoriesTask(Category categoryToSelect)
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Categories.Clear();

                    foreach (var category in this.Database.GetCategories())
                        this.Categories.Add(category);

                    if (categoryToSelect != null)
                        this.SelectedCategory = categoryToSelect;
                });
            });
        }

        private Task LoadCustomersTask(Customer customerToSelect)
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Customers.Clear();

                    foreach (var customer in this.Database.GetCustomers())
                        this.Customers.Add(customer);

                    if (customerToSelect != null)
                        this.SelectedCustomer = customerToSelect;
                });
            });
        }

        private Task LoadStoresTask(Store storeToSelect)
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Stores.Clear();

                    foreach (var store in this.Database.GetStores())
                        this.Stores.Add(store);

                    if (storeToSelect != null)
                        this.SelectedStore = storeToSelect;
                });
            });
        }

        private Task LoadSuppliersTask(Supplier supplierToSelect)
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Suppliers.Clear();

                    foreach (var supplier in this.Database.GetSuppliers())
                        this.Suppliers.Add(supplier);

                    if (supplierToSelect != null)
                        this.SelectedSupplier = supplierToSelect;
                });
            });
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
