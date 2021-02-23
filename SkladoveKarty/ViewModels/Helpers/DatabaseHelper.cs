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

        public void AddItem(Item newItem)
        {
            this.db.Items.Add(newItem);

            this.db.SaveChanges();
        }

        public void UpdateItem(Item newItem)
        {
            var item = this.db.Items.Where(i => i.Id == newItem.Id).SingleOrDefault();

            item.Name = newItem.Name;
            item.Movement = newItem.Movement;
            item.Qty = newItem.Qty;
            item.Price = newItem.Price;
            item.Invoice = newItem.Invoice;
            item.Customer = newItem.Customer;

            this.db.SaveChanges();
        }

        public void DeleteItem(Item newItem)
        {
            var item = this.db.Items.Where(i => i.Id == newItem.Id).SingleOrDefault();

            this.db.Items.Remove(item);

            this.db.SaveChanges();
        }

        public void UpdateStorageCard(StorageCard newStorageCard)
        {
            var storageCard = this.db.StorageCards.Where(s => s.Id == newStorageCard.Id).SingleOrDefault();

            storageCard.Account = newStorageCard.Account;
            storageCard.Name = newStorageCard.Name;
            storageCard.Category = newStorageCard.Category;
            storageCard.Store = newStorageCard.Store;

            this.db.SaveChanges();
        }
    }
}
