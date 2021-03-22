namespace SkladoveKarty.UnitTests
{
    using System;
    using System.Collections.Generic;
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
        private DatabaseHelper databaseHelper;

        [SetUp]
        public void SetUp()
        {
            var mockContext = new Mock<DatabaseContext>();

            var accounts = new List<Account>
            {
                new Account { DateTime = this.dateTime, Name = "account1" },
                new Account { DateTime = this.dateTime, Name = "account3" },
                new Account { DateTime = this.dateTime, Name = "account2" },
            };
            var mockSetAccounts = GetMockSet(accounts.AsQueryable());
            mockContext.Setup(m => m.Accounts).Returns(mockSetAccounts.Object);

            var categories = new List<Category>
            {
                new Category { DateTime = this.dateTime, Name = "category1" },
                new Category { DateTime = this.dateTime, Name = "category3" },
                new Category { DateTime = this.dateTime, Name = "category2" },
            };
            var mockSetCategories = GetMockSet(categories.AsQueryable());
            mockContext.Setup(m => m.Categories).Returns(mockSetCategories.Object);

            var customers = new List<Customer>
            {
                new Customer { DateTime = this.dateTime, Name = "customer1" },
                new Customer { DateTime = this.dateTime, Name = "customer3" },
                new Customer { DateTime = this.dateTime, Name = "customer2" },
            };
            var mockSetCustomers = GetMockSet(customers.AsQueryable());
            mockContext.Setup(m => m.Customers).Returns(mockSetCustomers.Object);

            var stores = new List<Store>
            {
                new Store { DateTime = this.dateTime, Name = "store1" },
                new Store { DateTime = this.dateTime, Name = "store3" },
                new Store { DateTime = this.dateTime, Name = "store2" },
            };
            var mockSetStores = GetMockSet(stores.AsQueryable());
            mockContext.Setup(m => m.Stores).Returns(mockSetStores.Object);

            var suppliers = new List<Supplier>
            {
                new Supplier { DateTime = this.dateTime, Name = "supplier1" },
                new Supplier { DateTime = this.dateTime, Name = "supplier3" },
                new Supplier { DateTime = this.dateTime, Name = "supplier2" },
            };
            var mockSetSuppliers = GetMockSet(suppliers.AsQueryable());
            mockContext.Setup(m => m.Suppliers).Returns(mockSetSuppliers.Object);

            var storageCards = new List<StorageCard>
            {
                new StorageCard
                {
                    DateTime = this.dateTime,
                    Name = "storageCard1",
                    Account = accounts[0],
                    Category = categories[0],
                    Store = stores[0],
                },
                new StorageCard
                {
                    DateTime = this.dateTime,
                    Name = "storageCard3",
                    Account = accounts[1],
                    Category = categories[1],
                    Store = stores[1],
                },
                new StorageCard
                {
                    DateTime = this.dateTime,
                    Name = "storageCard2",
                    Account = accounts[2],
                    Category = categories[2],
                    Store = stores[2],
                },
            };
            storageCards = new List<StorageCard>
            {
                new StorageCard
                {
                    Id = 1,
                    DateTime = this.dateTime,
                    Name = "storageCard1",
                    Account = accounts[0],
                    Category = categories[0],
                    Store = stores[0],
                },
                new StorageCard
                {
                    Id = 2,
                    DateTime = this.dateTime,
                    Name = "storageCard3",
                    Account = accounts[1],
                    Category = categories[1],
                    Store = stores[1],
                },
                new StorageCard
                {
                    Id = 3,
                    DateTime = this.dateTime,
                    Name = "storageCard2",
                    Account = accounts[2],
                    Category = categories[2],
                    Store = stores[2],
                },
            };
            var mockSetStorageCards = GetMockSet(storageCards.AsQueryable());
            mockContext.Setup(m => m.StorageCards).Returns(mockSetStorageCards.Object);

            var storageCardSuppliers = new List<StorageCardSupplier>
            {
                new StorageCardSupplier { DateTime = this.dateTime, StorageCard = storageCards[0], Supplier = suppliers[0] },
            };
            var mockSetStorageCardSuppliers = GetMockSet(storageCardSuppliers.AsQueryable());
            mockContext.Setup(m => m.StorageCardSuppliers).Returns(mockSetStorageCardSuppliers.Object);

            this.databaseHelper = new DatabaseHelper(mockContext.Object);
        }

        [Test]
        public void GetAccounts_WhenCalled_ReturnAccounts()
        {
            var accounts = this.databaseHelper.GetAccounts();

            Assert.That(accounts.Count, Is.EqualTo(3));

            Assert.That(accounts[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(accounts[0].Name, Is.EqualTo("account1"));

            Assert.That(accounts[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(accounts[1].Name, Is.EqualTo("account2"));

            Assert.That(accounts[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(accounts[2].Name, Is.EqualTo("account3"));
        }

        [Test]
        public void GetCategories_WhenCalled_ReturnCategories()
        {
            var categories = this.databaseHelper.GetCategories();

            Assert.That(categories.Count, Is.EqualTo(3));

            Assert.That(categories[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(categories[0].Name, Is.EqualTo("category1"));

            Assert.That(categories[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(categories[1].Name, Is.EqualTo("category2"));

            Assert.That(categories[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(categories[2].Name, Is.EqualTo("category3"));
        }

        [Test]
        public void GetCustomers_WhenCalled_ReturnCustomers()
        {
            var customers = this.databaseHelper.GetCustomers();

            Assert.That(customers.Count, Is.EqualTo(3));

            Assert.That(customers[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(customers[0].Name, Is.EqualTo("customer1"));

            Assert.That(customers[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(customers[1].Name, Is.EqualTo("customer2"));

            Assert.That(customers[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(customers[2].Name, Is.EqualTo("customer3"));
        }

        [Test]
        public void GetStores_WhenCalled_ReturnStores()
        {
            var stores = this.databaseHelper.GetStores();

            Assert.That(stores.Count, Is.EqualTo(3));

            Assert.That(stores[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(stores[0].Name, Is.EqualTo("store1"));

            Assert.That(stores[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(stores[1].Name, Is.EqualTo("store2"));

            Assert.That(stores[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(stores[2].Name, Is.EqualTo("store3"));
        }

        [Test]
        public void GetSuppliers_WhenCalled_ReturnSuppliers()
        {
            var suppliers = this.databaseHelper.GetSuppliers();

            Assert.That(suppliers.Count, Is.EqualTo(3));

            Assert.That(suppliers[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(suppliers[0].Name, Is.EqualTo("supplier1"));

            Assert.That(suppliers[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(suppliers[1].Name, Is.EqualTo("supplier2"));

            Assert.That(suppliers[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(suppliers[2].Name, Is.EqualTo("supplier3"));
        }

        [Test]
        public void GetStorageCards_WhenCalled_ReturnStorageCards()
        {
            var storageCards = this.databaseHelper.GetStorageCards();

            Assert.That(storageCards.Count, Is.EqualTo(3));

            Assert.That(storageCards[0].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Name, Is.EqualTo("storageCard1"));
            Assert.That(storageCards[0].Account, Is.Not.Null);
            Assert.That(storageCards[0].Account.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Account.Name, Is.EqualTo("account1"));
            Assert.That(storageCards[0].Category, Is.Not.Null);
            Assert.That(storageCards[0].Category.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Category.Name, Is.EqualTo("category1"));
            Assert.That(storageCards[0].Store, Is.Not.Null);
            Assert.That(storageCards[0].Store.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[0].Store.Name, Is.EqualTo("store1"));

            Assert.That(storageCards[1].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Name, Is.EqualTo("storageCard2"));
            Assert.That(storageCards[1].Account, Is.Not.Null);
            Assert.That(storageCards[1].Account.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Account.Name, Is.EqualTo("account2"));
            Assert.That(storageCards[1].Category, Is.Not.Null);
            Assert.That(storageCards[1].Category.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Category.Name, Is.EqualTo("category2"));
            Assert.That(storageCards[1].Store, Is.Not.Null);
            Assert.That(storageCards[1].Store.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[1].Store.Name, Is.EqualTo("store2"));

            Assert.That(storageCards[2].DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Name, Is.EqualTo("storageCard3"));
            Assert.That(storageCards[2].Account, Is.Not.Null);
            Assert.That(storageCards[2].Account.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Account.Name, Is.EqualTo("account3"));
            Assert.That(storageCards[2].Category, Is.Not.Null);
            Assert.That(storageCards[2].Category.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Category.Name, Is.EqualTo("category3"));
            Assert.That(storageCards[2].Store, Is.Not.Null);
            Assert.That(storageCards[2].Store.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCards[2].Store.Name, Is.EqualTo("store3"));
        }

        [Test]
        public void GetStorageCardSuppliers_WhenCalled_ReturnStorageCardSuppliers()
        {
            var storageCardsSuppliers = this.databaseHelper.GetStorageCardSuppliers();

            Assert.That(storageCardsSuppliers.Count, Is.EqualTo(1));

            Assert.That(storageCardsSuppliers[0].StorageCard, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].StorageCard.Name, Is.EqualTo("storageCard1"));
            Assert.That(storageCardsSuppliers[0].Supplier, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].Supplier.Name, Is.EqualTo("supplier1"));
        }

        [Test]
        public void GetStorageCardSuppliers_WhenCalledWithArgument_ReturnStorageCardSuppliers()
        {
            var storageCard = this.databaseHelper.GetStorageCards().First();
            var storageCardsSuppliers = this.databaseHelper.GetStorageCardSuppliers(storageCard);

            Assert.That(storageCardsSuppliers.Count, Is.EqualTo(1));

            Assert.That(storageCardsSuppliers[0].StorageCard, Is.EqualTo(storageCard));
            Assert.That(storageCardsSuppliers[0].Supplier, Is.Not.Null);
            Assert.That(storageCardsSuppliers[0].Supplier.Name, Is.EqualTo("supplier1"));
        }

        [Test]
        public void GetStorageCard_WhenCalled_ReturnStorageCard()
        {
            var storageCard = this.databaseHelper.GetStorageCard(1);

            Assert.That(storageCard, Is.Not.Null);
            Assert.That(storageCard.Id, Is.EqualTo(1));
            Assert.That(storageCard.DateTime, Is.EqualTo(this.dateTime));
            Assert.That(storageCard.Name, Is.EqualTo("storageCard1"));
        }

        [Test]
        public void GetStorageCardSupplier_WhenCalled_ReturnStorageCardSupplier()
        {
            var storageCard = this.databaseHelper.GetStorageCards().First();
            var supplier = this.databaseHelper.GetSuppliers().First();
            var storageCardSupplier = this.databaseHelper.GetStorageCardSupplier(storageCard, supplier);

            Assert.That(storageCardSupplier, Is.Not.Null);
            Assert.That(storageCardSupplier.DateTime, Is.EqualTo(this.dateTime));
        }

        [Test]
        public void Add_WhenCalled_AddAccount()
        {
            var account = new Account { DateTime = this.dateTime, Name = "account4" };

            this.databaseHelper.Add(account);

            var accounts = this.databaseHelper.GetAccounts();
        }

        private static Mock<DbSet<T>> GetMockSet<T>(IQueryable<T> data)
            where T : class
        {
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }
    }
}
