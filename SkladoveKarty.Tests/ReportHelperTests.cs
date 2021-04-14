namespace SkladoveKarty.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    [TestFixture]
    public class ReportHelperTests
    {
        private List<Item> items;

        [SetUp]
        public void SetUp()
        {
            this.items = new()
            {
                new Item() { Movement = 1, Price = 50, Qty = 5 },
                new Item() { Movement = 1, Price = 20, Qty = 10 },
                new Item() { Movement = -1, Price = 70, Qty = 5 },
                new Item() { Movement = -1, Price = 50, Qty = 1 },
            };
        }

        [Test]
        [TestCase(true, 1, 450)]
        [TestCase(true, -1, 400)]
        [TestCase(true, 0, 0)]
        [TestCase(false, 1, 0)]
        public void GetStorageCardItemsPrice_WhenCalled_ReturnPrice(bool useItems, int movement, decimal expectedResult)
        {
            var result = ReportHelper.GetStorageCardItemsPrice(useItems ? this.items : null, movement);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(true, 270)]
        [TestCase(false, 0)]
        public void GetStorageCardItemsPrice_WhenCalled_ReturnPrice(bool useItems, decimal expectedResult)
        {
            var result = ReportHelper.GetStorageCardItemsPrice(useItems ? this.items : null);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(true, 9)]
        [TestCase(false, 0)]
        public void GetStorageCardItemsQty_WhenCalled_ReturnQty(bool useItems, decimal expectedResult)
        {
            var result = ReportHelper.GetStorageCardItemsQty(useItems ? this.items : null);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
