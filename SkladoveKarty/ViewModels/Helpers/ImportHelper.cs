namespace SkladoveKarty.ViewModels.Helpers
{
    using System;
    using System.Collections.Generic;
    using SkladoveKarty.Models;

    public class ImportHelper
    {
        private readonly DatabaseHelper databaseHelper;

        private readonly List<Account> addedAccounts = new();
        private readonly List<Category> addedCategories = new();
        private readonly List<Customer> addedCustomers = new();
        private readonly List<StorageCard> addedStorageCards = new();
        private readonly List<StorageCardSupplier> addedStorageCardSuppliers = new();
        private readonly List<Store> addedStores = new();
        private readonly List<Supplier> addedSuppliers = new();

        public ImportHelper(DatabaseHelper databaseHelper)
        {
            if (databaseHelper == null) throw new ArgumentNullException(nameof(databaseHelper));

            this.databaseHelper = databaseHelper;
        }

        public void ImportItems(List<ImportExportItem> importItems)
        {
            if (importItems == null) throw new ArgumentNullException(nameof(importItems));

            try
            {
                foreach (var importItem in importItems)
                {
                    this.ImportItem(importItem);
                }
            }
            catch
            {
                this.databaseHelper.RollBack();
                throw;
            }
        }

        public void ImportSuppliers(List<ImportExportSupplier> importSuppliers)
        {
            if (importSuppliers == null) throw new ArgumentNullException(nameof(importSuppliers));

            try
            {
                foreach (var importSupplier in importSuppliers)
                {
                    this.ImportSupplier(importSupplier);
                }
            }
            catch
            {
                this.databaseHelper.RollBack();
                throw;
            }
        }

        private void ImportItem(ImportExportItem importItem)
        {
            var account = this.databaseHelper.GetAccount(importItem.AccountName, this.addedAccounts);

            if (account == null)
            {
                account = this.databaseHelper.Add(new Account { DateTime = DateTime.Now, Name = importItem.AccountName });
                this.addedAccounts.Add(account);
            }

            var category = this.databaseHelper.GetCategory(importItem.CategoryName, this.addedCategories);

            if (category == null)
            {
                category = this.databaseHelper.Add(new Category { DateTime = DateTime.Now, Name = importItem.CategoryName });
                this.addedCategories.Add(category);
            }

            var store = this.databaseHelper.GetStore(importItem.StoreName, this.addedStores);

            if (store == null)
            {
                store = this.databaseHelper.Add(new Store { DateTime = DateTime.Now, Name = importItem.StoreName });
                this.addedStores.Add(store);
            }

            var storageCard = this.databaseHelper.GetStorageCard(importItem.StorageCardName, this.addedStorageCards);

            if (storageCard == null)
            {
                storageCard = this.databaseHelper.Add(new StorageCard
                {
                    DateTime = DateTime.Now,
                    Name = importItem.StorageCardName,
                    Account = account,
                    Category = category,
                    Store = store,
                });
                this.addedStorageCards.Add(storageCard);
            }

            var customer = default(Customer);

            if (!string.IsNullOrEmpty(importItem.ItemCustomerName))
            {
                customer = this.databaseHelper.GetCustomer(importItem.ItemCustomerName, this.addedCustomers);

                if (customer == null)
                {
                    customer = this.databaseHelper.Add(new Customer { DateTime = DateTime.Now, Name = importItem.ItemCustomerName });
                    this.addedCustomers.Add(customer);
                }
            }

            this.databaseHelper.Add(new Item
            {
                DateTime = importItem.ItemDateTime,
                Name = importItem.ItemName,
                Movement = importItem.ItemMovement,
                Qty = importItem.ItemQty,
                Price = importItem.ItemPrice,
                Invoice = importItem.ItemInvoice,
                Customer = customer,
                StorageCard = storageCard,
            });
        }

        private void ImportSupplier(ImportExportSupplier importSupplier)
        {
            var storageCard = this.databaseHelper.GetStorageCard(importSupplier.StorageCardName, this.addedStorageCards)
                ?? throw new ArgumentException($"Skladová karta '{importSupplier.StorageCardName}' neexistuje.");

            var supplier = this.databaseHelper.GetSupplier(importSupplier.SupplierName, this.addedSuppliers);

            if (supplier == null)
            {
                supplier = this.databaseHelper.Add(new Supplier { DateTime = DateTime.Now, Name = importSupplier.SupplierName });
                this.addedSuppliers.Add(supplier);
            }

            var storageCardSupplier = this.databaseHelper.GetStorageCardSupplier(storageCard, supplier, this.addedStorageCardSuppliers);

            if (storageCardSupplier != null)
                throw new ArgumentException($"Skladová karta '{importSupplier.StorageCardName}' již obsahuje dodavatele '{importSupplier.SupplierName}'.");

            this.addedStorageCardSuppliers.Add(
                this.databaseHelper.Add(
                    new StorageCardSupplier { DateTime = DateTime.Now, StorageCard = storageCard, Supplier = supplier }));
        }
    }
}