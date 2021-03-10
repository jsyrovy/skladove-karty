namespace SkladoveKarty.ViewModels.Helpers
{
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

        public List<Store> GetStores()
        {
            return this.db.Stores.OrderBy(s => s.Name).ToList();
        }

        public List<Supplier> GetSuppliers()
        {
            return this.db.Suppliers.OrderBy(s => s.Name).ToList();
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

        public void UpdateAccount(Account newAccount)
        {
            var account = this.db.Accounts.Where(i => i.Id == newAccount.Id).Single();

            account.Name = newAccount.Name;

            this.db.SaveChanges();
        }

        public void UpdateCategory(Category newCategory)
        {
            var category = this.db.Categories.Where(i => i.Id == newCategory.Id).Single();

            category.Name = newCategory.Name;

            this.db.SaveChanges();
        }

        public void UpdateCustomer(Customer newCustomer)
        {
            var customer = this.db.Customers.Where(i => i.Id == newCustomer.Id).Single();

            customer.Name = newCustomer.Name;

            this.db.SaveChanges();
        }

        public void UpdateItem(Item newItem)
        {
            var item = this.db.Items.Where(i => i.Id == newItem.Id).Single();

            item.Name = newItem.Name;
            item.Movement = newItem.Movement;
            item.Qty = newItem.Qty;
            item.Price = newItem.Price;
            item.Invoice = newItem.Invoice;
            item.Customer = newItem.Customer;

            this.db.SaveChanges();
        }

        public void UpdateStore(Store newStore)
        {
            var store = this.db.Stores.Where(i => i.Id == newStore.Id).Single();

            store.Name = newStore.Name;

            this.db.SaveChanges();
        }

        public void UpdateSupplier(Supplier newSupplier)
        {
            var supplier = this.db.Suppliers.Where(i => i.Id == newSupplier.Id).Single();

            supplier.Name = newSupplier.Name;

            this.db.SaveChanges();
        }

        public void UpdateStorageCard(StorageCard newStorageCard)
        {
            var storageCard = this.db.StorageCards.Where(s => s.Id == newStorageCard.Id).Single();

            storageCard.Account = newStorageCard.Account;
            storageCard.Name = newStorageCard.Name;
            storageCard.Category = newStorageCard.Category;
            storageCard.Store = newStorageCard.Store;

            this.db.SaveChanges();
        }

        public void DeleteAccount(Account newAccount)
        {
            var account = this.db.Accounts.Where(i => i.Id == newAccount.Id).Single();

            this.db.Accounts.Remove(account);

            this.db.SaveChanges();
        }

        public void DeleteCategory(Category newCategory)
        {
            var category = this.db.Categories.Where(i => i.Id == newCategory.Id).Single();

            this.db.Categories.Remove(category);

            this.db.SaveChanges();
        }

        public void DeleteCustomer(Customer newCustomer)
        {
            var customer = this.db.Customers.Where(i => i.Id == newCustomer.Id).Single();

            this.db.Customers.Remove(customer);

            this.db.SaveChanges();
        }

        public void DeleteStore(Store newStore)
        {
            var store = this.db.Stores.Where(i => i.Id == newStore.Id).Single();

            this.db.Stores.Remove(store);

            this.db.SaveChanges();
        }

        public void DeleteSupplier(Supplier newSupplier)
        {
            var supplier = this.db.Suppliers.Where(i => i.Id == newSupplier.Id).Single();

            this.db.Suppliers.Remove(supplier);

            this.db.SaveChanges();
        }

        public void DeleteItem(Item newItem)
        {
            var item = this.db.Items.Where(i => i.Id == newItem.Id).Single();

            this.db.Items.Remove(item);

            this.db.SaveChanges();
        }
    }
}
