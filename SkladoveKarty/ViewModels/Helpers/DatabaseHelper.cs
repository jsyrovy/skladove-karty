namespace SkladoveKarty.ViewModels.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SkladoveKarty.Models;

    public class DatabaseHelper
    {
        private readonly DatabaseContext db = new();

        public List<Account> GetAccounts()
        {
            return this.db.Accounts.OrderBy(a => a.Name).ToList();
        }

        public List<Category> GetCategories()
        {
            return this.db.Categories.OrderBy(c => c.Name).ToList();
        }

        public List<Customer> GetCustomers()
        {
            return this.db.Customers.OrderBy(c => c.Name).ToList();
        }

        public List<StorageCard> GetStorageCards()
        {
            return this.db.StorageCards
                .Include(s => s.Account)
                .Include(s => s.Category)
                .Include(s => s.Store)
                .Include(s => s.Items)
                .ThenInclude(i => i.Customer)
                .Include(s => s.StorageCardSuppliers)
                .ThenInclude(s => s.Supplier)
                .OrderBy(s => s.Name)
                .ToList();
        }

        public List<StorageCardSupplier> GetStorageCardSuppliers(StorageCard storageCard)
        {
            return this.db.StorageCardSuppliers.Include(s => s.Supplier).Where(s => s.StorageCard == storageCard).ToList();
        }

        public List<Store> GetStores()
        {
            return this.db.Stores.OrderBy(s => s.Name).ToList();
        }

        public List<Supplier> GetSuppliers()
        {
            return this.db.Suppliers.OrderBy(s => s.Name).ToList();
        }

        public StorageCard GetStorageCard(long id)
        {
            return this.db.StorageCards.Where(s => s.Id == id).SingleOrDefault();
        }

        public StorageCardSupplier GetStorageCardSupplier(StorageCard storageCard, Supplier supplier)
        {
            return this.db.StorageCardSuppliers.Where(s => s.StorageCard == storageCard && s.Supplier == supplier).SingleOrDefault();
        }

        public void AddAccount(Account newAccount)
        {
            this.db.Accounts.Add(newAccount);

            this.db.SaveChanges();
        }

        public void AddCategory(Category newCategory)
        {
            this.db.Categories.Add(newCategory);

            this.db.SaveChanges();
        }

        public void AddCustomer(Customer newCustomer)
        {
            this.db.Customers.Add(newCustomer);

            this.db.SaveChanges();
        }

        public void AddItem(Item newItem)
        {
            this.db.Items.Add(newItem);

            this.db.SaveChanges();
        }

        public void AddStorageCardSupplier(StorageCardSupplier newStorageCardSupplier)
        {
            this.db.StorageCardSuppliers.Add(newStorageCardSupplier);

            this.db.SaveChanges();
        }

        public void AddStore(Store newStore)
        {
            this.db.Stores.Add(newStore);

            this.db.SaveChanges();
        }

        public void AddSupplier(Supplier newSupplier)
        {
            this.db.Suppliers.Add(newSupplier);

            this.db.SaveChanges();
        }

        public void DeleteAccount(Account account)
        {
            var assignedStorageCard = this.db.StorageCards.Where(s => s.Account == account).FirstOrDefault();

            if (assignedStorageCard != null)
                throw new InvalidOperationException($"Účet '{account.Name}' nelze smazat. Je přiřazen ke skladové kartě '{assignedStorageCard.Name}'.");

            this.db.Accounts.Remove(account);

            this.db.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            var assignedStorageCard = this.db.StorageCards.Where(s => s.Category == category).FirstOrDefault();

            if (assignedStorageCard != null)
                throw new InvalidOperationException($"Kategorii '{category.Name}' nelze smazat. Je přiřazena ke skladové kartě '{assignedStorageCard.Name}'.");

            this.db.Categories.Remove(category);

            this.db.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            var assignedItem = this.db.Items.Include(i => i.StorageCard).Where(i => i.Customer == customer).FirstOrDefault();

            if (assignedItem != null)
                throw new InvalidOperationException($"Dodavatele '{customer.Name}' nelze smazat. Je přiřazen k položce '{assignedItem.Name}' ve skladové kartě '{assignedItem.StorageCard.Name}'.");

            this.db.Customers.Remove(customer);

            this.db.SaveChanges();
        }

        public void DeleteStorageCardSupplier(StorageCardSupplier storageCardSupplier)
        {
            this.db.StorageCardSuppliers.Remove(storageCardSupplier);

            this.db.SaveChanges();
        }

        public void DeleteStore(Store store)
        {
            var assignedStorageCard = this.db.StorageCards.Where(s => s.Store == store).FirstOrDefault();

            if (assignedStorageCard != null)
                throw new InvalidOperationException($"Sklad '{store.Name}' nelze smazat. Je přiřazen ke skladové kartě '{assignedStorageCard.Name}'.");

            this.db.Stores.Remove(store);

            this.db.SaveChanges();
        }

        public void DeleteSupplier(Supplier supplier)
        {
            var assignedStorageCardSupplier = this.db.StorageCardSuppliers
                .Include(s => s.StorageCard)
                .Where(s => s.Supplier == supplier)
                .FirstOrDefault();

            if (assignedStorageCardSupplier != null)
            {
                throw new InvalidOperationException(
                    $"Dodavatele '{supplier.Name}' nelze smazat. Je přiřazen ke skladové kartě '{assignedStorageCardSupplier.StorageCard.Name}'.");
            }

            this.db.Suppliers.Remove(supplier);

            this.db.SaveChanges();
        }

        public void DeleteItem(Item item)
        {
            this.db.Items.Remove(item);

            this.db.SaveChanges();
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }
    }
}
