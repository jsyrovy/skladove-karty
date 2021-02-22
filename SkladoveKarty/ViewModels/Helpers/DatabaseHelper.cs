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

        public List<StorageCard> GetStorageCards()
        {
            return this.db.StorageCards.OrderBy(s => s.Name).ToList();
        }

        public List<Item> GetItems()
        {
            return this.db.Items
                .Include(i => i.StorageCard)
                .Include(i => i.Customer)
                .OrderBy(i => i.DateTime)
                .ThenBy(i => i.Name)
                .ToList();
        }

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

        public List<Store> GetStores()
        {
            return this.db.Stores.OrderBy(s => s.Name).ToList();
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
