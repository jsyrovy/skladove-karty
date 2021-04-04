namespace SkladoveKarty.UnitTests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    [TestFixture]
    public class ExportHelperTests
    {
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
