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
        public void GetBoolValue_ValueDoesntExist_ReturnFalse()
        {
            var result = SettingHelper.GetBoolValue(this.context, "not-exists");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GetBoolValue_ValueIsFalse_ReturnFalse()
        {
            var setting = new Setting()
            {
                Name = "name",
                Value = "false",
            };
            this.context.Settings.Add(setting);
            this.context.SaveChanges();

            var result = SettingHelper.GetBoolValue(this.context, setting.Name);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GetBoolValue_ValueIsTrue_ReturnTrue()
        {
            var setting = new Setting()
            {
                Name = "name",
                Value = "TRUE",
            };
            this.context.Settings.Add(setting);
            this.context.SaveChanges();

            var result = SettingHelper.GetBoolValue(this.context, setting.Name);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GetBoolValue_ValueIsntBoolean_ThrowException()
        {
            var setting = new Setting()
            {
                Name = "name",
                Value = "not-bool",
            };
            this.context.Settings.Add(setting);
            this.context.SaveChanges();

            Assert.Throws<InvalidOperationException>(() => SettingHelper.GetBoolValue(this.context, setting.Name));
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
