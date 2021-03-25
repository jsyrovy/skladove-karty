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
            this.context.AddRange(
                new Account { DateTime = this.dateTime, Name = "account1" },
                new Account { DateTime = this.dateTime, Name = "account3" },
                new Account { DateTime = this.dateTime, Name = "account2" });

            this.context.SaveChanges();

            var databaseHelper = new DatabaseHelper(this.context);
            var accounts = databaseHelper.GetAccounts();

            Assert.That(accounts.Count, Is.EqualTo(3));

            Assert.That(accounts[0].Id, Is.Not.Null);
            Assert.That(accounts[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(accounts[0].Name, Is.EqualTo("account1"));

            Assert.That(accounts[1].Id, Is.Not.Null);
            Assert.That(accounts[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(accounts[1].Name, Is.EqualTo("account2"));

            Assert.That(accounts[2].Id, Is.Not.Null);
            Assert.That(accounts[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(accounts[2].Name, Is.EqualTo("account3"));
        }

        [Test]
        public void GetCategories_WhenCalled_ReturnCategories()
        {
            this.context.AddRange(
                new Category { DateTime = this.dateTime, Name = "category1" },
                new Category { DateTime = this.dateTime, Name = "category3" },
                new Category { DateTime = this.dateTime, Name = "category2" });

            this.context.SaveChanges();

            var databaseHelper = new DatabaseHelper(this.context);
            var categories = databaseHelper.GetCategories();

            Assert.That(categories.Count, Is.EqualTo(3));

            Assert.That(categories[0].Id, Is.Not.Null);
            Assert.That(categories[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(categories[0].Name, Is.EqualTo("category1"));

            Assert.That(categories[1].Id, Is.Not.Null);
            Assert.That(categories[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(categories[1].Name, Is.EqualTo("category2"));

            Assert.That(categories[2].Id, Is.Not.Null);
            Assert.That(categories[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(categories[2].Name, Is.EqualTo("category3"));
        }

        [Test]
        public void GetCustomers_WhenCalled_ReturnCustomers()
        {
            this.context.AddRange(
                new Customer { DateTime = this.dateTime, Name = "customer1" },
                new Customer { DateTime = this.dateTime, Name = "customer3" },
                new Customer { DateTime = this.dateTime, Name = "customer2" });

            this.context.SaveChanges();

            var databaseHelper = new DatabaseHelper(this.context);
            var customers = databaseHelper.GetCustomers();

            Assert.That(customers.Count, Is.EqualTo(3));

            Assert.That(customers[0].Id, Is.Not.Null);
            Assert.That(customers[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(customers[0].Name, Is.EqualTo("customer1"));

            Assert.That(customers[1].Id, Is.Not.Null);
            Assert.That(customers[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(customers[1].Name, Is.EqualTo("customer2"));

            Assert.That(customers[2].Id, Is.Not.Null);
            Assert.That(customers[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(customers[2].Name, Is.EqualTo("customer3"));
        }

        [Test]
        public void GetStores_WhenCalled_ReturnStores()
        {
            this.context.AddRange(
                new Store { DateTime = this.dateTime, Name = "store1" },
                new Store { DateTime = this.dateTime, Name = "store3" },
                new Store { DateTime = this.dateTime, Name = "store2" });

            this.context.SaveChanges();

            var databaseHelper = new DatabaseHelper(this.context);
            var stores = databaseHelper.GetStores();

            Assert.That(stores.Count, Is.EqualTo(3));

            Assert.That(stores[0].Id, Is.Not.Null);
            Assert.That(stores[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(stores[0].Name, Is.EqualTo("store1"));

            Assert.That(stores[1].Id, Is.Not.Null);
            Assert.That(stores[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(stores[1].Name, Is.EqualTo("store2"));

            Assert.That(stores[2].Id, Is.Not.Null);
            Assert.That(stores[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(stores[2].Name, Is.EqualTo("store3"));
        }

        [Test]
        public void GetSuppliers_WhenCalled_ReturnSuppliers()
        {
            this.context.AddRange(
                new Supplier { DateTime = this.dateTime, Name = "supplier1" },
                new Supplier { DateTime = this.dateTime, Name = "supplier3" },
                new Supplier { DateTime = this.dateTime, Name = "supplier2" });

            this.context.SaveChanges();

            var databaseHelper = new DatabaseHelper(this.context);
            var suppliers = databaseHelper.GetSuppliers();

            Assert.That(suppliers.Count, Is.EqualTo(3));

            Assert.That(suppliers[0].Id, Is.Not.Null);
            Assert.That(suppliers[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(suppliers[0].Name, Is.EqualTo("supplier1"));

            Assert.That(suppliers[1].Id, Is.Not.Null);
            Assert.That(suppliers[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(suppliers[1].Name, Is.EqualTo("supplier2"));

            Assert.That(suppliers[2].Id, Is.Not.Null);
            Assert.That(suppliers[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(suppliers[2].Name, Is.EqualTo("supplier3"));
        }

        [Test]
        public void GetStorageCards_WhenCalled_ReturnStorageCards()
        {
            this.context.AddRange(
                new StorageCard
                {
                    DateTime = this.dateTime,
                    Name = "storageCard1",
                    Account = new Account { DateTime = this.dateTime, Name = "account1" },
                    Category = new Category { DateTime = this.dateTime, Name = "category1" },
                    Store = new Store { DateTime = this.dateTime, Name = "store1" },
                },
                new StorageCard
                {
                    DateTime = this.dateTime,
                    Name = "storageCard3",
                    Account = new Account { DateTime = this.dateTime, Name = "account3" },
                    Category = new Category { DateTime = this.dateTime, Name = "category3" },
                    Store = new Store { DateTime = this.dateTime, Name = "store3" },
                },
                new StorageCard
                {
                    DateTime = this.dateTime,
                    Name = "storageCard2",
                    Account = new Account { DateTime = this.dateTime, Name = "account2" },
                    Category = new Category { DateTime = this.dateTime, Name = "category2" },
                    Store = new Store { DateTime = this.dateTime, Name = "store2" },
                });

            this.context.SaveChanges();

            var databaseHelper = new DatabaseHelper(this.context);
            var storageCards = databaseHelper.GetStorageCards();

            Assert.That(storageCards.Count, Is.EqualTo(3));

            Assert.That(storageCards[0].Id, Is.Not.Null);
            Assert.That(storageCards[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Name, Is.EqualTo("storageCard1"));
            Assert.That(storageCards[0].Account, Is.Not.Null);
            Assert.That(storageCards[0].Account.Id, Is.Not.Null);
            Assert.That(storageCards[0].Account.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Account.Name, Is.EqualTo("account1"));
            Assert.That(storageCards[0].Category, Is.Not.Null);
            Assert.That(storageCards[0].Category.Id, Is.Not.Null);
            Assert.That(storageCards[0].Category.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Category.Name, Is.EqualTo("category1"));
            Assert.That(storageCards[0].Store, Is.Not.Null);
            Assert.That(storageCards[0].Store.Id, Is.Not.Null);
            Assert.That(storageCards[0].Store.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Store.Name, Is.EqualTo("store1"));

            Assert.That(storageCards[1].Id, Is.Not.Null);
            Assert.That(storageCards[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Name, Is.EqualTo("storageCard2"));
            Assert.That(storageCards[1].Account, Is.Not.Null);
            Assert.That(storageCards[1].Account.Id, Is.Not.Null);
            Assert.That(storageCards[1].Account.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Account.Name, Is.EqualTo("account2"));
            Assert.That(storageCards[1].Category, Is.Not.Null);
            Assert.That(storageCards[1].Category.Id, Is.Not.Null);
            Assert.That(storageCards[1].Category.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Category.Name, Is.EqualTo("category2"));
            Assert.That(storageCards[1].Store, Is.Not.Null);
            Assert.That(storageCards[1].Store.Id, Is.Not.Null);
            Assert.That(storageCards[1].Store.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Store.Name, Is.EqualTo("store2"));

            Assert.That(storageCards[2].Id, Is.Not.Null);
            Assert.That(storageCards[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Name, Is.EqualTo("storageCard3"));
            Assert.That(storageCards[2].Account, Is.Not.Null);
            Assert.That(storageCards[2].Account.Id, Is.Not.Null);
            Assert.That(storageCards[2].Account.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Account.Name, Is.EqualTo("account3"));
            Assert.That(storageCards[2].Category, Is.Not.Null);
            Assert.That(storageCards[2].Category.Id, Is.Not.Null);
            Assert.That(storageCards[2].Category.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Category.Name, Is.EqualTo("category3"));
            Assert.That(storageCards[2].Store, Is.Not.Null);
            Assert.That(storageCards[2].Store.Id, Is.Not.Null);
            Assert.That(storageCards[2].Store.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Store.Name, Is.EqualTo("store3"));
        }

        [Test]
        public void GetStorageCardSuppliers_WhenCalled_ReturnStorageCardSuppliers()
        {
            this.context.Add(
                new StorageCardSupplier
                {
                    DateTime = this.dateTime,
                    StorageCard = new StorageCard { DateTime = this.dateTime, Name = "storageCard" },
                    Supplier = new Supplier { DateTime = this.dateTime, Name = "supplier" },
                });

            this.context.SaveChanges();

            var databaseHelper = new DatabaseHelper(this.context);
            var storageCardsSuppliers = databaseHelper.GetStorageCardSuppliers();

            Assert.That(storageCardsSuppliers.Count, Is.EqualTo(1));

            Assert.That(storageCardsSuppliers[0].Id, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCardsSuppliers[0].StorageCard, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].StorageCard.Id, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].StorageCard.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCardsSuppliers[0].StorageCard.Name, Is.EqualTo("storageCard"));
            Assert.That(storageCardsSuppliers[0].Supplier, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].Supplier.Id, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].Supplier.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCardsSuppliers[0].Supplier.Name, Is.EqualTo("supplier"));
        }

        [Test]
        public void GetStorageCardSuppliers_WhenCalledWithArgument_ReturnStorageCardSuppliers()
        {
            this.context.Add(
                new StorageCardSupplier
                {
                    DateTime = this.dateTime,
                    StorageCard = new StorageCard
                    {
                        DateTime = this.dateTime,
                        Name = "storageCard",
                        Account = new Account { DateTime = this.dateTime, Name = "account" },
                        Category = new Category { DateTime = this.dateTime, Name = "category" },
                        Store = new Store { DateTime = this.dateTime, Name = "store" },
                    },
                    Supplier = new Supplier
                    {
                        DateTime = this.dateTime,
                        Name = "supplier",
                    },
                });

            this.context.SaveChanges();

            var databaseHelper = new DatabaseHelper(this.context);
            var storageCard = databaseHelper.GetStorageCards().First();
            var storageCardsSuppliers = databaseHelper.GetStorageCardSuppliers(storageCard);

            Assert.That(storageCardsSuppliers.Count, Is.EqualTo(1));

            Assert.That(storageCardsSuppliers[0].StorageCard, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].StorageCard.Id, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].StorageCard.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCardsSuppliers[0].StorageCard.Name, Is.EqualTo("storageCard"));
            Assert.That(storageCardsSuppliers[0].Supplier, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].Supplier.Id, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].Supplier.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCardsSuppliers[0].Supplier.Name, Is.EqualTo("supplier"));
        }

        [Test]
        public void GetStorageCard_WhenCalled_ReturnStorageCard()
        {
            this.context.Add(
                new StorageCard
                {
                    DateTime = this.dateTime,
                    Name = "storageCard",
                    Account = new Account { DateTime = this.dateTime, Name = "account" },
                    Category = new Category { DateTime = this.dateTime, Name = "category" },
                    Store = new Store { DateTime = this.dateTime, Name = "store" },
                });

            this.context.SaveChanges();

            var databaseHelper = new DatabaseHelper(this.context);
            var storageCard = databaseHelper.GetStorageCard(1);

            Assert.That(storageCard, Is.Not.Null);
            Assert.That(storageCard.Id, Is.EqualTo(1));
            Assert.That(storageCard.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCard.Name, Is.EqualTo("storageCard"));
        }

        [Test]
        public void GetStorageCardSupplier_WhenCalled_ReturnStorageCardSupplier()
        {
            this.context.Add(
                new StorageCardSupplier
                {
                    DateTime = this.dateTime,
                    StorageCard = new StorageCard
                    {
                        DateTime = this.dateTime,
                        Name = "storageCard",
                        Account = new Account { DateTime = this.dateTime, Name = "account" },
                        Category = new Category { DateTime = this.dateTime, Name = "category" },
                        Store = new Store { DateTime = this.dateTime, Name = "store" },
                    },
                    Supplier = new Supplier
                    {
                        DateTime = this.dateTime,
                        Name = "supplier",
                    },
                });

            this.context.SaveChanges();

            var databaseHelper = new DatabaseHelper(this.context);
            var storageCard = databaseHelper.GetStorageCards().First();
            var supplier = databaseHelper.GetSuppliers().First();
            var storageCardSupplier = databaseHelper.GetStorageCardSupplier(storageCard, supplier);

            Assert.That(storageCardSupplier, Is.Not.Null);
            Assert.That(storageCardSupplier.Id, Is.Not.Null);
            Assert.That(storageCardSupplier.DateTime, Is.EqualTo(this.dateTime));

            Assert.That(storageCardSupplier.StorageCard, Is.Not.Null);
            Assert.That(storageCardSupplier.StorageCard.Id, Is.Not.Null);
            Assert.That(storageCardSupplier.StorageCard.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCardSupplier.StorageCard.Name, Is.EqualTo("storageCard"));

            Assert.That(storageCardSupplier.Supplier, Is.Not.Null);
            Assert.That(storageCardSupplier.Supplier.Id, Is.Not.Null);
            Assert.That(storageCardSupplier.Supplier.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCardSupplier.Supplier.Name, Is.EqualTo("supplier"));
        }

        [Test]
        public void Add_WhenCalled_AddAccount()
        {
            var account = new Account { DateTime = DateTime.Now, Name = "account" };

            var databaseHelper = new DatabaseHelper(this.context);
            databaseHelper.Add(account);

            var accounts = this.context.Accounts;

            Assert.That(accounts.Count(), Is.EqualTo(1));
            Assert.That(accounts.First().Id, Is.Not.Null);
            Assert.That(accounts.First().DateTime, Is.EqualTo(account.DateTime));
            Assert.That(accounts.First().Name, Is.EqualTo(account.Name));
        }
    }
}
