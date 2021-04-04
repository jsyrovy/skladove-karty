namespace SkladoveKarty.UnitTests
{
    using System;
    using NUnit.Framework;
    using SkladoveKarty.ViewModels.Helpers;

    [TestFixture]
    public class ExceptionHelperTests
    {
        [Test]
        public void GetCompleteExceptionMessage_WhenCalled_ReturnMessage()
        {
            var exception1 = new Exception("message1");
            var exception2 = new ApplicationException("message2", exception1);

            var result = ExceptionHelper.GetCompleteExceptionMessage(exception2);

            Assert.That(result, Is.EqualTo($"{exception2.Message}{Environment.NewLine}{Environment.NewLine}{exception1.Message}"));
        }
    }
}
