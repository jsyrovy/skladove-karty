namespace SkladoveKarty.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
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
