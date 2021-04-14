namespace SkladoveKarty.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using SkladoveKarty.ViewModels.Helpers;

    [TestFixture]
    public class HtmlHelperTests
    {
        private string template;
        private Dictionary<string, string> model;

        [SetUp]
        public void SetUp()
        {
            this.template = "{{ key }}";
            this.model = new Dictionary<string, string> { ["key"] = "value" };
        }

        [Test]
        public void Generate_WhenCalled_ReturnHtml()
        {
            var result = HtmlHelper.Generate(this.template, this.model);

            Assert.That(result, Is.EqualTo("value"));
        }

        [Test]
        public void Generate_TemplateIsNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => HtmlHelper.Generate(null, this.model));
        }

        [Test]
        public void Generate_ModelIsNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => HtmlHelper.Generate(this.template, null));
        }
    }
}
