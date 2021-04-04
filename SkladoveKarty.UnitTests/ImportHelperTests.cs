namespace SkladoveKarty.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Moq;
    using NUnit.Framework;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    [TestFixture]
    public class ImportHelperTests
    {
        private DatabaseContext databaseContext;
        private DatabaseHelper databaseHelper;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.databaseContext = new DatabaseContext(options);
            this.databaseHelper = new DatabaseHelper(this.databaseContext);
        }

        [TearDown]
        public void TearDown()
        {
            this.databaseContext.Database.EnsureDeleted();
        }

        [Test]
        public void ImportHelper_DatabaseContextIsNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new ImportHelper(null));
        }

        [Test]
        public void ImportItems_WhenCalled_ImportItems()
        {
            var importItem1 = new ImportExportItem
            {
                StorageCardName = "storageCard",
                AccountName = "account",
                CategoryName = "category",
                StoreName = "store",
                ItemDateTime = DateTime.Now,
                ItemName = "item1",
                ItemMovement = 1,
                ItemQty = 2,
                ItemPrice = 3,
                ItemInvoice = "invoice1",
                ItemCustomerName = null,
            };
            var importItem2 = new ImportExportItem
            {
                StorageCardName = "storageCard",
                AccountName = "account",
                CategoryName = "category",
                StoreName = "store",
                ItemDateTime = DateTime.Now,
                ItemName = "item2",
                ItemMovement = -1,
                ItemQty = 5,
                ItemPrice = 10,
                ItemInvoice = "invoice2",
                ItemCustomerName = "customer",
            };
            var importItems = new List<ImportExportItem>
            {
                importItem1,
                importItem2,
            };

            var importHelper = new ImportHelper(this.databaseHelper);
            importHelper.ImportItems(importItems);
            this.databaseContext.SaveChanges();
            var storageCard = this.databaseContext.StorageCards.Single();

            Assert.That(storageCard.Name, Is.EqualTo("storageCard"));
            Assert.That(storageCard.Account.Name, Is.EqualTo("account"));
            Assert.That(storageCard.Category.Name, Is.EqualTo("category"));
            Assert.That(storageCard.Store.Name, Is.EqualTo("store"));
            Assert.That(storageCard.Items.Count, Is.EqualTo(2));
            Assert.That(storageCard.Items[0].DateTime, Is.EqualTo(importItem1.ItemDateTime));
            Assert.That(storageCard.Items[0].Name, Is.EqualTo(importItem1.ItemName));
            Assert.That(storageCard.Items[0].Movement, Is.EqualTo(importItem1.ItemMovement));
            Assert.That(storageCard.Items[0].Qty, Is.EqualTo(importItem1.ItemQty));
            Assert.That(storageCard.Items[0].Price, Is.EqualTo(importItem1.ItemPrice));
            Assert.That(storageCard.Items[0].Invoice, Is.EqualTo(importItem1.ItemInvoice));
            Assert.That(storageCard.Items[0].Customer?.Name, Is.EqualTo(importItem1.ItemCustomerName));
            Assert.That(storageCard.Items[1].DateTime, Is.EqualTo(importItem2.ItemDateTime));
            Assert.That(storageCard.Items[1].Name, Is.EqualTo(importItem2.ItemName));
            Assert.That(storageCard.Items[1].Movement, Is.EqualTo(importItem2.ItemMovement));
            Assert.That(storageCard.Items[1].Qty, Is.EqualTo(importItem2.ItemQty));
            Assert.That(storageCard.Items[1].Price, Is.EqualTo(importItem2.ItemPrice));
            Assert.That(storageCard.Items[1].Invoice, Is.EqualTo(importItem2.ItemInvoice));
            Assert.That(storageCard.Items[1].Customer?.Name, Is.EqualTo(importItem2.ItemCustomerName));
        }

        [Test]
        public void ImportItems_ArgumentIsNull_ThrowException()
        {
            var databaseContext = new Mock<IDatabaseContext>();
            databaseContext.Setup(d => d.ChangeTracker).Returns<ChangeTracker>(null);
            databaseContext.Setup(d => d.Accounts).Throws<Exception>();
            var databaseHelper = new DatabaseHelper(databaseContext.Object);
            var importHelper = new ImportHelper(databaseHelper);

            Assert.Throws<Exception>(() => importHelper.ImportItems(new List<ImportExportItem> { new() }));
        }

        [Test]
        public void ImportItems_UnexpectedError_ThrowException()
        {
            var importHelper = new ImportHelper(this.databaseHelper);

            Assert.Throws<ArgumentNullException>(() => importHelper.ImportItems(null));
        }

        [Test]
        public void ImportSuppliers_WhenCalled_ImportSuppliers()
        {
            this.databaseContext.StorageCards.Add(new StorageCard { Name = "storageCard1" });
            this.databaseContext.StorageCards.Add(new StorageCard { Name = "storageCard2" });
            this.databaseContext.SaveChanges();

            var importSupplier1 = new ImportExportSupplier
            {
                StorageCardName = "storageCard1",
                SupplierName = "supplier1",
            };
            var importSupplier2 = new ImportExportSupplier
            {
                StorageCardName = "storageCard1",
                SupplierName = "supplier2",
            };
            var importSupplier3 = new ImportExportSupplier
            {
                StorageCardName = "storageCard2",
                SupplierName = "supplier2",
            };
            var importSuppliers = new List<ImportExportSupplier>
            {
                importSupplier1,
                importSupplier2,
                importSupplier3,
            };

            var importHelper = new ImportHelper(this.databaseHelper);
            importHelper.ImportSuppliers(importSuppliers);
            this.databaseContext.SaveChanges();
            var storageCardSuppliers = this.databaseContext.StorageCardSuppliers
                .OrderBy(s => s.StorageCard.Name).ThenBy(s => s.Supplier.Name).ToList();

            Assert.That(storageCardSuppliers.Count, Is.EqualTo(3));
            Assert.That(storageCardSuppliers[0].StorageCard, Is.Not.Null);
            Assert.That(storageCardSuppliers[0].StorageCard.Name, Is.EqualTo("storageCard1"));
            Assert.That(storageCardSuppliers[0].Supplier, Is.Not.Null);
            Assert.That(storageCardSuppliers[0].Supplier.Name, Is.EqualTo("supplier1"));
            Assert.That(storageCardSuppliers[1].StorageCard, Is.Not.Null);
            Assert.That(storageCardSuppliers[1].StorageCard.Name, Is.EqualTo("storageCard1"));
            Assert.That(storageCardSuppliers[1].Supplier, Is.Not.Null);
            Assert.That(storageCardSuppliers[1].Supplier.Name, Is.EqualTo("supplier2"));
            Assert.That(storageCardSuppliers[2].StorageCard, Is.Not.Null);
            Assert.That(storageCardSuppliers[2].StorageCard.Name, Is.EqualTo("storageCard2"));
            Assert.That(storageCardSuppliers[2].Supplier, Is.Not.Null);
            Assert.That(storageCardSuppliers[2].Supplier.Name, Is.EqualTo("supplier2"));
        }

        [Test]
        public void ImportSuppliers_ArgumentIsNull_ThrowException()
        {
            var importHelper = new ImportHelper(this.databaseHelper);

            Assert.Throws<ArgumentNullException>(() => importHelper.ImportSuppliers(null));
        }

        [Test]
        public void ImportSuppliers_StorageCardDoesntExist_ThrowException()
        {
            var importSuppliers = new List<ImportExportSupplier>
            {
                new ImportExportSupplier
                {
                    StorageCardName = "storageCard",
                    SupplierName = "supplier",
                },
            };

            var importHelper = new ImportHelper(this.databaseHelper);

            Assert.Throws<ArgumentException>(() => importHelper.ImportSuppliers(importSuppliers));
        }

        [Test]
        public void ImportSuppliers_StorageCardSupplierExists_ThrowException()
        {
            this.databaseContext.StorageCards.Add(new StorageCard { Name = "storageCard" });
            this.databaseContext.SaveChanges();
            var importSupplier1 = new ImportExportSupplier
            {
                StorageCardName = "storageCard",
                SupplierName = "supplier",
            };
            var importSupplier2 = new ImportExportSupplier
            {
                StorageCardName = "storageCard",
                SupplierName = "supplier",
            };
            var importSuppliers = new List<ImportExportSupplier>
            {
                importSupplier1,
                importSupplier2,
            };

            var importHelper = new ImportHelper(this.databaseHelper);

            Assert.Throws<ArgumentException>(() => importHelper.ImportSuppliers(importSuppliers));
        }
    }
}
