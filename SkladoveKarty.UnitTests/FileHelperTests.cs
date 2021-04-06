namespace SkladoveKarty.UnitTests
{
    using NUnit.Framework;
    using SkladoveKarty.ViewModels.Helpers;

    [TestFixture]
    public class FileHelperTests
    {
        [Test]
        [TestCase("file.csv", "file.csv")]
        [TestCase("file?.csv", "file.csv")]
        [TestCase("fi|le.csv", "file.csv")]
        [TestCase("f\\i/le.csv", "file.csv")]
        public void GetValidFileName_WhenCalled_ReturnName(string name, string expectedResult)
        {
            var result = FileHelper.GetValidFileName(name);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
