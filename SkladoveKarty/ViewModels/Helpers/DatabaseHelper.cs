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
        private readonly IDatabaseContext databaseContext;

        public DatabaseHelper(IDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Account> GetAccounts()
        {
            return this.databaseContext.Accounts.OrderBy(a => a.Name).ToList();
        }

        public List<Category> GetCategories()
        {
            return this.databaseContext.Categories.OrderBy(c => c.Name).ToList();
        }

        public List<Customer> GetCustomers()
        {
            return this.databaseContext.Customers.OrderBy(c => c.Name).ToList();
        }

        public List<StorageCard> GetStorageCards()
        {
            return this.databaseContext.StorageCards
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

        public List<StorageCardSupplier> GetStorageCardSuppliers()
        {
            return this.databaseContext.StorageCardSuppliers.Include(s => s.StorageCard).Include(s => s.Supplier).ToList();
        }

        public List<StorageCardSupplier> GetStorageCardSuppliers(StorageCard storageCard)
        {
            return this.databaseContext.StorageCardSuppliers.Include(s => s.Supplier).Where(s => s.StorageCard == storageCard).ToList();
        }

        public List<Store> GetStores()
        {
            return this.databaseContext.Stores.OrderBy(s => s.Name).ToList();
        }

        public List<Supplier> GetSuppliers()
        {
            return this.databaseContext.Suppliers.OrderBy(s => s.Name).ToList();
        }

        public StorageCard GetStorageCard(long id)
        {
            return this.databaseContext.StorageCards.Where(s => s.Id == id).SingleOrDefault();
        }

        public StorageCardSupplier GetStorageCardSupplier(StorageCard storageCard, Supplier supplier)
        {
            return this.databaseContext.StorageCardSuppliers.Where(s => s.StorageCard == storageCard && s.Supplier == supplier).SingleOrDefault();
        }

        public T Add<T>(T entity)
        {
            this.databaseContext.Add(entity);

            return entity;
        }

        public void AddAndSave(object entity)
        {
            this.databaseContext.Add(entity);

            this.databaseContext.SaveChanges();
        }

        public void DeleteAccount(Account account)
        {
            var assignedStorageCard = this.databaseContext.StorageCards.Where(s => s.Account == account).FirstOrDefault();

            if (assignedStorageCard != null)
                throw new InvalidOperationException($"Účet '{account.Name}' nelze smazat. Je přiřazen ke skladové kartě '{assignedStorageCard.Name}'.");

            this.databaseContext.Accounts.Remove(account);

            this.databaseContext.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            var assignedStorageCard = this.databaseContext.StorageCards.Where(s => s.Category == category).FirstOrDefault();

            if (assignedStorageCard != null)
                throw new InvalidOperationException($"Kategorii '{category.Name}' nelze smazat. Je přiřazena ke skladové kartě '{assignedStorageCard.Name}'.");

            this.databaseContext.Categories.Remove(category);

            this.databaseContext.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            var assignedItem = this.databaseContext.Items.Include(i => i.StorageCard).Where(i => i.Customer == customer).FirstOrDefault();

            if (assignedItem != null)
                throw new InvalidOperationException($"Dodavatele '{customer.Name}' nelze smazat. Je přiřazen k položce '{assignedItem.Name}' ve skladové kartě '{assignedItem.StorageCard.Name}'.");

            this.databaseContext.Customers.Remove(customer);

            this.databaseContext.SaveChanges();
        }

        public void DeleteStorageCardSupplier(StorageCardSupplier storageCardSupplier)
        {
            this.databaseContext.StorageCardSuppliers.Remove(storageCardSupplier);

            this.databaseContext.SaveChanges();
        }

        public void DeleteStorageCard(StorageCard storageCard)
        {
            var storageCardSuppliers = this.databaseContext.StorageCardSuppliers.Where(s => s.StorageCard == storageCard);
            this.databaseContext.StorageCardSuppliers.RemoveRange(storageCardSuppliers);

            var items = this.databaseContext.Items.Where(i => i.StorageCard == storageCard);
            this.databaseContext.Items.RemoveRange(items);

            this.databaseContext.StorageCards.Remove(storageCard);

            this.databaseContext.SaveChanges();
        }

        public void DeleteStore(Store store)
        {
            var assignedStorageCard = this.databaseContext.StorageCards.Where(s => s.Store == store).FirstOrDefault();

            if (assignedStorageCard != null)
                throw new InvalidOperationException($"Sklad '{store.Name}' nelze smazat. Je přiřazen ke skladové kartě '{assignedStorageCard.Name}'.");

            this.databaseContext.Stores.Remove(store);

            this.databaseContext.SaveChanges();
        }

        public void DeleteSupplier(Supplier supplier)
        {
            var assignedStorageCardSupplier = this.databaseContext.StorageCardSuppliers
                .Include(s => s.StorageCard)
                .Where(s => s.Supplier == supplier)
                .FirstOrDefault();

            if (assignedStorageCardSupplier != null)
            {
                throw new InvalidOperationException(
                    $"Dodavatele '{supplier.Name}' nelze smazat. Je přiřazen ke skladové kartě '{assignedStorageCardSupplier.StorageCard.Name}'.");
            }

            this.databaseContext.Suppliers.Remove(supplier);

            this.databaseContext.SaveChanges();
        }

        public void DeleteItem(Item item)
        {
            this.databaseContext.Items.Remove(item);

            this.databaseContext.SaveChanges();
        }

        public void SaveChanges()
        {
            this.databaseContext.SaveChanges();
        }

        public void RollBack()
        {
            var changedEntries = this.databaseContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
    }
}
