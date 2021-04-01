namespace SkladoveKarty.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    [TestFixture]
    public class DatabaseHelperTests
    {
        private readonly DateTime dateTime = DateTime.Now;
        private DatabaseContext context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.context = new DatabaseContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }

        [Test]
        public void GetAccounts_WhenCalled_ReturnAccounts()
        {
            this.AddTestData();

            var databaseHelper = new DatabaseHelper(this.context);
            var accounts = databaseHelper.GetAccounts();

            Assert.That(accounts.Count, Is.EqualTo(3));
            Assert.That(accounts[0].Id, Is.Not.Zero);
            Assert.That(accounts[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(accounts[0].Name, Is.EqualTo("account1"));
            Assert.That(accounts[1].Id, Is.Not.Zero);
            Assert.That(accounts[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(accounts[1].Name, Is.EqualTo("account2"));
            Assert.That(accounts[2].Id, Is.Not.Zero);
            Assert.That(accounts[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(accounts[2].Name, Is.EqualTo("account3"));
        }

        [Test]
        public void GetCategories_WhenCalled_ReturnCategories()
        {
            this.AddTestData();

            var databaseHelper = new DatabaseHelper(this.context);
            var categories = databaseHelper.GetCategories();

            Assert.That(categories.Count, Is.EqualTo(3));
            Assert.That(categories[0].Id, Is.Not.Zero);
            Assert.That(categories[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(categories[0].Name, Is.EqualTo("category1"));
            Assert.That(categories[1].Id, Is.Not.Zero);
            Assert.That(categories[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(categories[1].Name, Is.EqualTo("category2"));
            Assert.That(categories[2].Id, Is.Not.Zero);
            Assert.That(categories[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(categories[2].Name, Is.EqualTo("category3"));
        }

        [Test]
        public void GetCustomers_WhenCalled_ReturnCustomers()
        {
            this.AddTestData();

            var databaseHelper = new DatabaseHelper(this.context);
            var customers = databaseHelper.GetCustomers();

            Assert.That(customers.Count, Is.EqualTo(3));
            Assert.That(customers[0].Id, Is.Not.Zero);
            Assert.That(customers[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(customers[0].Name, Is.EqualTo("customer1"));
            Assert.That(customers[1].Id, Is.Not.Zero);
            Assert.That(customers[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(customers[1].Name, Is.EqualTo("customer2"));
            Assert.That(customers[2].Id, Is.Not.Zero);
            Assert.That(customers[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(customers[2].Name, Is.EqualTo("customer3"));
        }

        [Test]
        public void GetStores_WhenCalled_ReturnStores()
        {
            this.AddTestData();

            var databaseHelper = new DatabaseHelper(this.context);
            var stores = databaseHelper.GetStores();

            Assert.That(stores.Count, Is.EqualTo(3));
            Assert.That(stores[0].Id, Is.Not.Zero);
            Assert.That(stores[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(stores[0].Name, Is.EqualTo("store1"));
            Assert.That(stores[1].Id, Is.Not.Zero);
            Assert.That(stores[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(stores[1].Name, Is.EqualTo("store2"));
            Assert.That(stores[2].Id, Is.Not.Zero);
            Assert.That(stores[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(stores[2].Name, Is.EqualTo("store3"));
        }

        [Test]
        public void GetSuppliers_WhenCalled_ReturnSuppliers()
        {
            this.AddTestData();

            var databaseHelper = new DatabaseHelper(this.context);
            var suppliers = databaseHelper.GetSuppliers();

            Assert.That(suppliers.Count, Is.EqualTo(3));
            Assert.That(suppliers[0].Id, Is.Not.Zero);
            Assert.That(suppliers[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(suppliers[0].Name, Is.EqualTo("supplier1"));
            Assert.That(suppliers[1].Id, Is.Not.Zero);
            Assert.That(suppliers[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(suppliers[1].Name, Is.EqualTo("supplier2"));
            Assert.That(suppliers[2].Id, Is.Not.Zero);
            Assert.That(suppliers[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(suppliers[2].Name, Is.EqualTo("supplier3"));
        }

        [Test]
        public void GetStorageCards_WhenCalled_ReturnStorageCards()
        {
            this.AddTestData();

            var databaseHelper = new DatabaseHelper(this.context);
            var storageCards = databaseHelper.GetStorageCards();

            Assert.That(storageCards.Count, Is.EqualTo(3));
            Assert.That(storageCards[0].Id, Is.Not.Zero);
            Assert.That(storageCards[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Name, Is.EqualTo("storageCard1"));
            Assert.That(storageCards[0].Account, Is.Not.Null);
            Assert.That(storageCards[0].Account.Id, Is.Not.Zero);
            Assert.That(storageCards[0].Account.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Account.Name, Is.EqualTo("account1"));
            Assert.That(storageCards[0].Category, Is.Not.Null);
            Assert.That(storageCards[0].Category.Id, Is.Not.Zero);
            Assert.That(storageCards[0].Category.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Category.Name, Is.EqualTo("category1"));
            Assert.That(storageCards[0].Store, Is.Not.Null);
            Assert.That(storageCards[0].Store.Id, Is.Not.Zero);
            Assert.That(storageCards[0].Store.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Store.Name, Is.EqualTo("store1"));
            Assert.That(storageCards[1].Id, Is.Not.Zero);
            Assert.That(storageCards[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Name, Is.EqualTo("storageCard2"));
            Assert.That(storageCards[1].Account, Is.Not.Null);
            Assert.That(storageCards[1].Account.Id, Is.Not.Zero);
            Assert.That(storageCards[1].Account.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Account.Name, Is.EqualTo("account2"));
            Assert.That(storageCards[1].Category, Is.Not.Null);
            Assert.That(storageCards[1].Category.Id, Is.Not.Zero);
            Assert.That(storageCards[1].Category.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Category.Name, Is.EqualTo("category2"));
            Assert.That(storageCards[1].Store, Is.Not.Null);
            Assert.That(storageCards[1].Store.Id, Is.Not.Zero);
            Assert.That(storageCards[1].Store.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Store.Name, Is.EqualTo("store2"));
            Assert.That(storageCards[2].Id, Is.Not.Zero);
            Assert.That(storageCards[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Name, Is.EqualTo("storageCard3"));
            Assert.That(storageCards[2].Account, Is.Not.Null);
            Assert.That(storageCards[2].Account.Id, Is.Not.Zero);
            Assert.That(storageCards[2].Account.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Account.Name, Is.EqualTo("account3"));
            Assert.That(storageCards[2].Category, Is.Not.Null);
            Assert.That(storageCards[2].Category.Id, Is.Not.Zero);
            Assert.That(storageCards[2].Category.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Category.Name, Is.EqualTo("category3"));
            Assert.That(storageCards[2].Store, Is.Not.Null);
            Assert.That(storageCards[2].Store.Id, Is.Not.Zero);
            Assert.That(storageCards[2].Store.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Store.Name, Is.EqualTo("store3"));
        }

        [Test]
        public void GetStorageCardSuppliers_WhenCalled_ReturnStorageCardSuppliers()
        {
            this.AddTestData();

            var databaseHelper = new DatabaseHelper(this.context);
            var storageCardsSuppliers = databaseHelper.GetStorageCardSuppliers();

            Assert.That(storageCardsSuppliers.Count, Is.EqualTo(6));
            Assert.That(storageCardsSuppliers.Where(s => s.Id == 0).Any(), Is.False);
            Assert.That(storageCardsSuppliers.Where(s => s.DateTime != this.dateTime).Any(), Is.False);
            Assert.That(storageCardsSuppliers.Where(s => s.StorageCard == null).Any(), Is.False);
            Assert.That(storageCardsSuppliers.Where(s => s.Supplier == null).Any(), Is.False);
        }

        [Test]
        public void GetStorageCardSuppliers_WhenCalledWithArgument_ReturnStorageCardSuppliers()
        {
            this.AddTestData();
            var storageCard = this.context.StorageCards.First();

            var databaseHelper = new DatabaseHelper(this.context);
            var storageCardsSuppliers = databaseHelper.GetStorageCardSuppliers(storageCard);

            Assert.That(storageCardsSuppliers.Count, Is.EqualTo(storageCard.StorageCardSuppliers.Count));
            Assert.That(storageCardsSuppliers.Where(s => s.Id == 0).Any(), Is.False);
            Assert.That(storageCardsSuppliers.Where(s => s.DateTime != this.dateTime).Any(), Is.False);
            Assert.That(storageCardsSuppliers.Where(s => s.StorageCard == null).Any(), Is.False);
            Assert.That(storageCardsSuppliers.Where(s => s.Supplier == null).Any(), Is.False);
        }

        [Test]
        public void GetStorageCard_WhenCalled_ReturnStorageCard()
        {
            this.AddTestData();
            var firstStorageCard = this.context.StorageCards.First();

            var databaseHelper = new DatabaseHelper(this.context);
            var storageCard = databaseHelper.GetStorageCard(firstStorageCard.Id);

            Assert.That(storageCard, Is.Not.Null);
            Assert.That(storageCard.Id, Is.EqualTo(firstStorageCard.Id));
            Assert.That(storageCard.DateTime, Is.EqualTo(firstStorageCard.DateTime));
            Assert.That(storageCard.Name, Is.EqualTo(firstStorageCard.Name));
        }

        [Test]
        public void GetStorageCardSupplier_WhenCalled_ReturnStorageCardSupplier()
        {
            this.AddTestData();
            var storageCard = this.context.StorageCards.First();
            var supplier = storageCard.StorageCardSuppliers.First().Supplier;

            var databaseHelper = new DatabaseHelper(this.context);
            var storageCardSupplier = databaseHelper.GetStorageCardSupplier(storageCard, supplier);

            Assert.That(storageCardSupplier, Is.Not.Null);
            Assert.That(storageCardSupplier.Id, Is.Not.Zero);
            Assert.That(storageCardSupplier.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCardSupplier.StorageCard, Is.EqualTo(storageCard));
            Assert.That(storageCardSupplier.Supplier, Is.EqualTo(supplier));
        }

        [Test]
        public void Add_WhenCalled_AddAccount()
        {
            var account = new Account { DateTime = this.dateTime, Name = "account" };

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.Add(account);
            var accountsBeforeSave = this.context.Accounts.ToList();
            this.context.SaveChanges();
            var accountsAfterSave = this.context.Accounts;

            Assert.That(accountsBeforeSave.Count, Is.Zero);

            Assert.That(accountsAfterSave.Count, Is.EqualTo(1));
            Assert.That(accountsAfterSave.First().Id, Is.Not.Zero);
            Assert.That(accountsAfterSave.First().DateTime, Is.EqualTo(account.DateTime));
            Assert.That(accountsAfterSave.First().Name, Is.EqualTo(account.Name));
        }

        [Test]
        public void Add_WhenCalled_AddAndSaveAccount()
        {
            var account = new Account { DateTime = this.dateTime, Name = "account" };

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.AddAndSave(account);
            var accounts = this.context.Accounts;

            Assert.That(accounts.Count, Is.EqualTo(1));
            Assert.That(accounts.First().Id, Is.Not.Zero);
            Assert.That(accounts.First().DateTime, Is.EqualTo(account.DateTime));
            Assert.That(accounts.First().Name, Is.EqualTo(account.Name));
        }

        [Test]
        public void SaveChanges_WhenCalled_SaveChanges()
        {
            var account = new Account { DateTime = this.dateTime, Name = "account" };
            this.context.Add(account);

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.SaveChanges();
            var accounts = this.context.Accounts;

            Assert.That(accounts.Count, Is.EqualTo(1));
            Assert.That(accounts.First().Id, Is.Not.Zero);
            Assert.That(accounts.First().DateTime, Is.EqualTo(account.DateTime));
            Assert.That(accounts.First().Name, Is.EqualTo(account.Name));
        }

        [Test]
        public void DeleteAccount_IsNotUsed_DeleteAccount()
        {
            this.context.Add(new Account());
            this.context.SaveChanges();
            var account = this.context.Accounts.First();

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.DeleteAccount(account);
            var accounts = this.context.Accounts;

            Assert.That(accounts.Count, Is.Zero);
        }

        [Test]
        public void DeleteAccount_IsUsed_ThrowException()
        {
            this.AddTestData();
            var account = this.context.Accounts.First();

            var databaseHelper = new DatabaseHelper(this.context);

            Assert.Throws<InvalidOperationException>(() => databaseHelper.DeleteAccount(account));
        }

        [Test]
        public void DeleteCategory_IsNotUsed_DeleteCategory()
        {
            this.context.Add(new Category());
            this.context.SaveChanges();
            var category = this.context.Categories.First();

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.DeleteCategory(category);
            var categories = this.context.Categories;

            Assert.That(categories.Count, Is.Zero);
        }

        [Test]
        public void DeleteCategory_IsUsed_ThrowException()
        {
            this.AddTestData();
            var category = this.context.Categories.First();

            var databaseHelper = new DatabaseHelper(this.context);

            Assert.Throws<InvalidOperationException>(() => databaseHelper.DeleteCategory(category));
        }

        [Test]
        public void DeleteCustomer_IsNotUsed_DeleteCustomer()
        {
            this.context.Add(new Customer());
            this.context.SaveChanges();
            var customer = this.context.Customers.First();

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.DeleteCustomer(customer);
            var customers = this.context.Customers;

            Assert.That(customers.Count, Is.Zero);
        }

        [Test]
        public void DeleteCustomer_IsUsed_ThrowException()
        {
            this.AddTestData();
            var customer = this.context.Customers.First();

            var databaseHelper = new DatabaseHelper(this.context);

            Assert.Throws<InvalidOperationException>(() => databaseHelper.DeleteCustomer(customer));
        }

        [Test]
        public void DeleteStorageCardSupplier_WhenCalled_DeleteStorageCardSupplier()
        {
            this.AddTestData();
            var storageCardSupplier = this.context.StorageCardSuppliers.First();
            var count = this.context.StorageCardSuppliers.Count();

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.DeleteStorageCardSupplier(storageCardSupplier);
            var storageCardSuppliers = this.context.StorageCardSuppliers;

            Assert.That(storageCardSuppliers.Count, Is.EqualTo(count - 1));
        }

        [Test]
        public void DeleteStorageCard_WhenCalled_DeleteStorageCard()
        {
            this.AddTestData();
            var storageCard = this.context.StorageCards.First();
            var count = this.context.StorageCards.Count();

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.DeleteStorageCard(storageCard);
            var storageCards = this.context.StorageCards;

            Assert.That(storageCards.Count, Is.EqualTo(count - 1));
        }

        [Test]
        public void DeleteStore_IsNotUsed_DeleteStore()
        {
            this.context.Add(new Store());
            this.context.SaveChanges();
            var store = this.context.Stores.First();

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.DeleteStore(store);
            var stores = this.context.Stores;

            Assert.That(stores.Count, Is.Zero);
        }

        [Test]
        public void DeleteStore_IsUsed_ThrowException()
        {
            this.AddTestData();
            var store = this.context.Stores.First();

            var databaseHelper = new DatabaseHelper(this.context);

            Assert.Throws<InvalidOperationException>(() => databaseHelper.DeleteStore(store));
        }

        [Test]
        public void DeleteSupplier_IsNotUsed_DeleteSupplier()
        {
            this.context.Add(new Supplier());
            this.context.SaveChanges();
            var supplier = this.context.Suppliers.First();

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.DeleteSupplier(supplier);
            var suppliers = this.context.Suppliers;

            Assert.That(suppliers.Count, Is.Zero);
        }

        [Test]
        public void DeleteSupplier_IsUsed_ThrowException()
        {
            this.AddTestData();
            var supplier = this.context.Suppliers.First();

            var databaseHelper = new DatabaseHelper(this.context);

            Assert.Throws<InvalidOperationException>(() => databaseHelper.DeleteSupplier(supplier));
        }

        [Test]
        public void DeleteItem_WhenCalled_DeleteItem()
        {
            this.AddTestData();
            var item = this.context.Items.First();
            var count = this.context.Items.Count();

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.DeleteItem(item);
            var items = this.context.Items;

            Assert.That(items.Count, Is.EqualTo(count - 1));
        }

        private void AddTestData()
        {
            var account1 = new Account { DateTime = this.dateTime, Name = "account1" };
            var account2 = new Account { DateTime = this.dateTime, Name = "account2" };
            var account3 = new Account { DateTime = this.dateTime, Name = "account3" };

            var category1 = new Category { DateTime = this.dateTime, Name = "category1" };
            var category2 = new Category { DateTime = this.dateTime, Name = "category2" };
            var category3 = new Category { DateTime = this.dateTime, Name = "category3" };

            var customer1 = new Customer { DateTime = this.dateTime, Name = "customer1" };
            var customer2 = new Customer { DateTime = this.dateTime, Name = "customer2" };
            var customer3 = new Customer { DateTime = this.dateTime, Name = "customer3" };

            var store1 = new Store { DateTime = this.dateTime, Name = "store1" };
            var store2 = new Store { DateTime = this.dateTime, Name = "store2" };
            var store3 = new Store { DateTime = this.dateTime, Name = "store3" };

            var supplier1 = new Supplier { DateTime = this.dateTime, Name = "supplier1" };
            var supplier2 = new Supplier { DateTime = this.dateTime, Name = "supplier2" };
            var supplier3 = new Supplier { DateTime = this.dateTime, Name = "supplier3" };

            var storageCard1 = new StorageCard
            {
                DateTime = this.dateTime,
                Name = "storageCard1",
                Account = account1,
                Category = category1,
                Store = store1,
            };

            var storageCard2 = new StorageCard
            {
                DateTime = this.dateTime,
                Name = "storageCard2",
                Account = account2,
                Category = category2,
                Store = store2,
            };

            var storageCard3 = new StorageCard
            {
                DateTime = this.dateTime,
                Name = "storageCard3",
                Account = account3,
                Category = category3,
                Store = store3,
            };

            var storageCardSupplier1 = new StorageCardSupplier
            {
                DateTime = this.dateTime,
                StorageCard = storageCard1,
                Supplier = supplier1,
            };

            var storageCardSupplier2 = new StorageCardSupplier
            {
                DateTime = this.dateTime,
                StorageCard = storageCard2,
                Supplier = supplier1,
            };

            var storageCardSupplier3 = new StorageCardSupplier
            {
                DateTime = this.dateTime,
                StorageCard = storageCard2,
                Supplier = supplier2,
            };

            var storageCardSupplier4 = new StorageCardSupplier
            {
                DateTime = this.dateTime,
                StorageCard = storageCard3,
                Supplier = supplier1,
            };

            var storageCardSupplier5 = new StorageCardSupplier
            {
                DateTime = this.dateTime,
                StorageCard = storageCard3,
                Supplier = supplier2,
            };

            var storageCardSupplier6 = new StorageCardSupplier
            {
                DateTime = this.dateTime,
                StorageCard = storageCard3,
                Supplier = supplier3,
            };

            var item1 = new Item
            {
                DateTime = this.dateTime,
                Name = "item1",
                Movement = 1,
                Qty = 1,
                Price = 1,
                Invoice = "invoice1",
                StorageCard = storageCard1,
                Customer = customer1,
            };

            this.context.AddRange(
                account1,
                account2,
                account3,
                category1,
                category2,
                category3,
                customer1,
                customer2,
                customer3,
                store1,
                store2,
                store3,
                supplier1,
                supplier2,
                supplier3,
                storageCard1,
                storageCard2,
                storageCard3,
                storageCardSupplier1,
                storageCardSupplier2,
                storageCardSupplier3,
                storageCardSupplier4,
                storageCardSupplier5,
                storageCardSupplier6,
                item1);

            this.context.SaveChanges();
        }
    }
}
