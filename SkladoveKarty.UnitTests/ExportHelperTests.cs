namespace SkladoveKarty.UnitTests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    [TestFixture]
    public class ExportHelperTests
    {
        private List<StorageCardSupplier> storageCardSuppliers;

        [SetUp]
        public void SetUp()
        {
            this.storageCardSuppliers = new()
            {
                new()
                {
                    StorageCard = new() { Name = "storageCard1" },
                    Supplier = new() { Name = "supplier1" },
                },
                new()
                {
                    StorageCard = new() { Name = "storageCard2" },
                    Supplier = new() { Name = "supplier2" },
                },
                new()
                {
                    StorageCard = new() { Name = "storageCard2" },
                    Supplier = new() { Name = "supplier3" },
                },
            };
        }

        [Test]
        public void GetExportSuppliers_WhenCalled_ReturnExportSuppliers()
        {
            var exportSuppliers = ExportHelper.GetExportSuppliers(this.storageCardSuppliers);

            Assert.That(exportSuppliers.Count, Is.EqualTo(3));

            Assert.That(exportSuppliers[0].StorageCardName, Is.EqualTo("storageCard1"));
            Assert.That(exportSuppliers[0].SupplierName, Is.EqualTo("supplier1"));

            Assert.That(exportSuppliers[1].StorageCardName, Is.EqualTo("storageCard2"));
            Assert.That(exportSuppliers[1].SupplierName, Is.EqualTo("supplier2"));

            Assert.That(exportSuppliers[2].StorageCardName, Is.EqualTo("storageCard2"));
            Assert.That(exportSuppliers[2].SupplierName, Is.EqualTo("supplier3"));
        }
    }
}
