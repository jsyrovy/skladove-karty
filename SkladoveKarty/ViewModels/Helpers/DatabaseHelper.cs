﻿namespace SkladoveKarty.ViewModels.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SkladoveKarty.Models;

    public class DatabaseHelper
    {
        private static Lazy<DatabaseHelper> instance;
        private readonly IDatabaseContext databaseContext;

        public DatabaseHelper(IDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public static DatabaseHelper GetInstance(IDatabaseContext databaseContext)
        {
            if (instance == null)
                instance = new Lazy<DatabaseHelper>(() => new DatabaseHelper(databaseContext));

            return instance.Value;
        }

        public Account GetAccount(string name, List<Account> addedAccounts = null)
        {
            return addedAccounts?.Where(a => a.Name == name).SingleOrDefault()
                ?? this.databaseContext.Accounts.Where(a => a.Name == name).SingleOrDefault();
        }

        public List<Account> GetAccounts()
        {
            return this.databaseContext.Accounts.OrderBy(a => a.Name).ToList();
        }

        public Category GetCategory(string name, List<Category> addedCategories = null)
        {
            return addedCategories?.Where(a => a.Name == name).SingleOrDefault()
                ?? this.databaseContext.Categories.Where(a => a.Name == name).SingleOrDefault();
        }

        public List<Category> GetCategories()
        {
            return this.databaseContext.Categories.OrderBy(c => c.Name).ToList();
        }

        public Customer GetCustomer(string name, List<Customer> addedCustomers = null)
        {
            return addedCustomers?.Where(c => c.Name == name).SingleOrDefault()
                ?? this.databaseContext.Customers.Where(c => c.Name == name).SingleOrDefault();
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

        public Store GetStore(string name, List<Store> addedStores = null)
        {
            return addedStores?.Where(a => a.Name == name).SingleOrDefault()
                ?? this.databaseContext.Stores.Where(a => a.Name == name).SingleOrDefault();
        }

        public List<Store> GetStores()
        {
            return this.databaseContext.Stores.OrderBy(s => s.Name).ToList();
        }

        public Supplier GetSupplier(string name, List<Supplier> addedSuppliers = null)
        {
            return addedSuppliers?.Where(s => s.Name == name).SingleOrDefault()
                ?? this.databaseContext.Suppliers.Where(s => s.Name == name).SingleOrDefault();
        }

        public List<Supplier> GetSuppliers()
        {
            return this.databaseContext.Suppliers.OrderBy(s => s.Name).ToList();
        }

        public StorageCard GetStorageCard(long id)
        {
            return this.databaseContext.StorageCards.Where(s => s.Id == id).SingleOrDefault();
        }

        public StorageCard GetStorageCard(string name, List<StorageCard> addedStorageCards = null)
        {
            return addedStorageCards?.Where(s => s.Name == name).SingleOrDefault()
                ?? this.databaseContext.StorageCards.Where(s => s.Name == name).SingleOrDefault();
        }

        public StorageCardSupplier GetStorageCardSupplier(StorageCard storageCard, Supplier supplier, List<StorageCardSupplier> addedStorageCardSuppliers = null)
        {
            return addedStorageCardSuppliers?.Where(s => s.StorageCard == storageCard && s.Supplier == supplier).SingleOrDefault()
                ?? this.databaseContext.StorageCardSuppliers.Where(s => s.StorageCard == storageCard && s.Supplier == supplier).SingleOrDefault();
        }

        public T Add<T>(T entity)
        {
            this.databaseContext.Add(entity);

            return entity;
        }

        public void DeleteAccount(Account account)
        {
            var assignedStorageCard = this.databaseContext.StorageCards.Where(s => s.Account == account).FirstOrDefault();

            if (assignedStorageCard != null)
                throw new InvalidOperationException($"Účet '{account.Name}' nelze smazat. Je přiřazen ke skladové kartě '{assignedStorageCard.Name}'.");

            this.databaseContext.Accounts.Remove(account);
        }

        public void DeleteCategory(Category category)
        {
            var assignedStorageCard = this.databaseContext.StorageCards.Where(s => s.Category == category).FirstOrDefault();

            if (assignedStorageCard != null)
                throw new InvalidOperationException($"Kategorii '{category.Name}' nelze smazat. Je přiřazena ke skladové kartě '{assignedStorageCard.Name}'.");

            this.databaseContext.Categories.Remove(category);
        }

        public void DeleteCustomer(Customer customer)
        {
            var assignedItem = this.databaseContext.Items.Include(i => i.StorageCard).Where(i => i.Customer == customer).FirstOrDefault();

            if (assignedItem != null)
                throw new InvalidOperationException($"Zákazníka '{customer.Name}' nelze smazat. Je přiřazen k položce '{assignedItem.Name}' ve skladové kartě '{assignedItem.StorageCard.Name}'.");

            this.databaseContext.Customers.Remove(customer);
        }

        public void DeleteStorageCardSupplier(StorageCardSupplier storageCardSupplier)
        {
            this.databaseContext.StorageCardSuppliers.Remove(storageCardSupplier);
        }

        public void DeleteStorageCard(StorageCard storageCard)
        {
            var storageCardSuppliers = this.databaseContext.StorageCardSuppliers.Where(s => s.StorageCard == storageCard);
            this.databaseContext.StorageCardSuppliers.RemoveRange(storageCardSuppliers);

            var items = this.databaseContext.Items.Where(i => i.StorageCard == storageCard);
            this.databaseContext.Items.RemoveRange(items);

            this.databaseContext.StorageCards.Remove(storageCard);
        }

        public void DeleteStore(Store store)
        {
            var assignedStorageCard = this.databaseContext.StorageCards.Where(s => s.Store == store).FirstOrDefault();

            if (assignedStorageCard != null)
                throw new InvalidOperationException($"Sklad '{store.Name}' nelze smazat. Je přiřazen ke skladové kartě '{assignedStorageCard.Name}'.");

            this.databaseContext.Stores.Remove(store);
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
        }

        public void DeleteItem(Item item)
        {
            this.databaseContext.Items.Remove(item);
        }

        public void DeleteAll()
        {
            this.databaseContext.Items.RemoveRange(this.databaseContext.Items);
            this.databaseContext.StorageCardSuppliers.RemoveRange(this.databaseContext.StorageCardSuppliers);
            this.databaseContext.StorageCards.RemoveRange(this.databaseContext.StorageCards);
            this.databaseContext.Accounts.RemoveRange(this.databaseContext.Accounts);
            this.databaseContext.Categories.RemoveRange(this.databaseContext.Categories);
            this.databaseContext.Customers.RemoveRange(this.databaseContext.Customers);
            this.databaseContext.Stores.RemoveRange(this.databaseContext.Stores);
            this.databaseContext.Suppliers.RemoveRange(this.databaseContext.Suppliers);
        }

        public void SaveChanges()
        {
            this.databaseContext.SaveChanges();
        }

        public void RollBack()
        {
            var changedEntries = this.databaseContext.ChangeTracker?
                .Entries().Where(e => e.State != EntityState.Unchanged).ToList();

            if (changedEntries == null) return;

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
