namespace SkladoveKarty.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    [TestFixture]
    public class SettingHelperTests
    {
        private DatabaseContext context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.context = new DatabaseContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }

        [Test]
        [TestCase(typeof(string))]
        [TestCase(typeof(bool))]
        public void GetValue_ValueDoesntExist_ReturnDefault<T>(T type)
        {
            var result = SettingHelper.GetValue<T>(this.context, "not-exists");

            Assert.That(result, Is.EqualTo(default(T)));
        }

        [Test]
        [TestCase(typeof(string))]
        [TestCase(typeof(bool))]
        public void GetValue_ValueIsDefault_ReturnDefault<T>(T type)
        {
            var setting = new Setting()
            {
                Name = "name",
                Value = default(T)?.ToString(),
            };
            this.context.Settings.Add(setting);
            this.context.SaveChanges();

            var result = SettingHelper.GetValue<T>(this.context, setting.Name);

            Assert.That(result, Is.EqualTo(default(T)));
        }

        [Test]
        public void GetValue_ValueIsntBoolean_ThrowException()
        {
            var setting = new Setting()
            {
                Name = "name",
                Value = "not-bool",
            };
            this.context.Settings.Add(setting);
            this.context.SaveChanges();

            Assert.Throws<FormatException>(() => SettingHelper.GetValue<bool>(this.context, setting.Name));
        }

        [Test]
        public void SaveValue_SettingDoesntExist_CreateSetting()
        {
            var value = true;
            SettingHelper.SaveValue(this.context, "name", value);
            this.context.SaveChanges();
            var result = this.context.Settings.Single();

            Assert.That(result.Value, Is.EqualTo(value.ToString()));
        }

        [Test]
        public void SaveValue_SettingExists_UpdateSetting()
        {
            var setting = new Setting()
            {
                Name = "name",
                Value = "false",
            };
            this.context.Settings.Add(setting);
            this.context.SaveChanges();
            var newValue = true;

            SettingHelper.SaveValue(this.context, setting.Name, newValue);
            this.context.SaveChanges();
            var result = this.context.Settings.Single();

            Assert.That(result.Value, Is.EqualTo(newValue.ToString()));
        }
    }
}
