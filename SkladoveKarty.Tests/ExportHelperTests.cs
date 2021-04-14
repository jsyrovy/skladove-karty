namespace SkladoveKarty.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    [TestFixture]
    public class ExportHelperTests
    {
        [Test]
        public void GetExportItems_WhenCalled_ReturnExportItems()
        {
            var account = new Account { Name = "account" };
            var category = new Category { Name = "category" };
            var store = new Store { Name = "store" };
            var item1 = new Item
            {
                DateTime = DateTime.Now,
                Name = "item1",
                Movement = 1,
                Qty = 5,
                Price = 10,
                Invoice = "invoice1",
                Customer = null,
            };
            var item2 = new Item
            {
                DateTime = DateTime.Now,
                Name = "item2",
                Movement = -1,
                Qty = 10,
                Price = 5,
                Invoice = "invoice2",
                Customer = new Customer { Name = "customer" },
            };
            var storageCard = new StorageCard
            {
                Account = account,
                Category = category,
                Store = store,
            };
            storageCard.Items.Add(item2);
            storageCard.Items.Add(item1);
            var storageCards = new List<StorageCard> { storageCard };

            var result = ExportHelper.GetExportItems(storageCards);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].StorageCardName, Is.EqualTo(storageCard.Name));
            Assert.That(result[0].AccountName, Is.EqualTo(account.Name));
            Assert.That(result[0].CategoryName, Is.EqualTo(category.Name));
            Assert.That(result[0].StoreName, Is.EqualTo(store.Name));
            Assert.That(result[0].ItemDateTime, Is.EqualTo(item1.DateTime));
            Assert.That(result[0].ItemName, Is.EqualTo(item1.Name));
            Assert.That(result[0].ItemMovement, Is.EqualTo(item1.Movement));
            Assert.That(result[0].ItemQty, Is.EqualTo(item1.Qty));
            Assert.That(result[0].ItemPrice, Is.EqualTo(item1.Price));
            Assert.That(result[0].ItemInvoice, Is.EqualTo(item1.Invoice));
            Assert.That(result[0].ItemCustomerName, Is.EqualTo(item1.Customer?.Name));
            Assert.That(result[1].StorageCardName, Is.EqualTo(storageCard.Name));
            Assert.That(result[1].AccountName, Is.EqualTo(account.Name));
            Assert.That(result[1].CategoryName, Is.EqualTo(category.Name));
            Assert.That(result[1].StoreName, Is.EqualTo(store.Name));
            Assert.That(result[1].ItemDateTime, Is.EqualTo(item2.DateTime));
            Assert.That(result[1].ItemName, Is.EqualTo(item2.Name));
            Assert.That(result[1].ItemMovement, Is.EqualTo(item2.Movement));
            Assert.That(result[1].ItemQty, Is.EqualTo(item2.Qty));
            Assert.That(result[1].ItemPrice, Is.EqualTo(item2.Price));
            Assert.That(result[1].ItemInvoice, Is.EqualTo(item2.Invoice));
            Assert.That(result[1].ItemCustomerName, Is.EqualTo(item2.Customer?.Name));
        }

        [Test]
        public void GetExportSuppliers_WhenCalled_ReturnExportSuppliers()
        {
            var storageCard = new StorageCard { Name = "storageCard" };
            var supplier1 = new Supplier { Name = "supplier1" };
            var supplier2 = new Supplier { Name = "supplier2" };
            var storageCardSuppliers = new List<StorageCardSupplier>
            {
                new StorageCardSupplier
                {
                    StorageCard = storageCard,
                    Supplier = supplier2,
                },
                new StorageCardSupplier
                {
                    StorageCard = storageCard,
                    Supplier = supplier1,
                },
            };

            var result = ExportHelper.GetExportSuppliers(storageCardSuppliers);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].StorageCardName, Is.EqualTo(storageCard.Name));
            Assert.That(result[0].SupplierName, Is.EqualTo(supplier1.Name));
            Assert.That(result[1].StorageCardName, Is.EqualTo(storageCard.Name));
            Assert.That(result[1].SupplierName, Is.EqualTo(supplier2.Name));
        }
    }
}
