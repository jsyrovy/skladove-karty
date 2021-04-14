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
        public void BackupDirectory_SettingDoesntExist_GetDefaultPath()
        {
            var helper = new SettingHelper(this.context);

            Assert.That(helper.BackupDirectory, Is.EqualTo(FileHelper.DefaultBackupDirectoryPath));
        }

        [Test]
        public void BackupDirectory_SettingExists_GetPath()
        {
            var setting = new Setting() { Name = SettingHelper.BackupDirectoryName, Value = "path" };
            this.context.Settings.Add(setting);
            this.context.SaveChanges();

            var helper = new SettingHelper(this.context);

            Assert.That(helper.BackupDirectory, Is.EqualTo(setting.Value));
        }

        [Test]
        public void BackupOnExit_SettingDoesntExist_GetDefaultValue()
        {
            var helper = new SettingHelper(this.context);

            Assert.That(helper.BackupOnExit, Is.False);
        }

        [Test]
        public void BackupOnExit_SettingValueIsTrue_GetTrue()
        {
            var setting = new Setting() { Name = SettingHelper.BackupOnExitName, Value = "true" };
            this.context.Settings.Add(setting);
            this.context.SaveChanges();

            var helper = new SettingHelper(this.context);

            Assert.That(helper.BackupOnExit, Is.True);
        }

        [Test]
        public void BackupDirectory_SettingDoesntExist_SetPath()
        {
            _ = new SettingHelper(this.context)
            {
                BackupDirectory = "path",
            };

            var result = this.context.Settings.Single().Value;

            Assert.That(result, Is.EqualTo("path"));
        }

        [Test]
        public void BackupDirectory_SettingExists_SetPath()
        {
            this.context.Settings.Add(new() { Name = SettingHelper.BackupDirectoryName, Value = "init" });
            this.context.SaveChanges();

            _ = new SettingHelper(this.context)
            {
                BackupDirectory = "path",
            };

            var result = this.context.Settings.Single().Value;

            Assert.That(result, Is.EqualTo("path"));
        }

        [Test]
        public void BackupOnExit_SettingDoesntExist_SetValue()
        {
            _ = new SettingHelper(this.context)
            {
                BackupOnExit = true,
            };

            var result = this.context.Settings.Single().Value;

            Assert.That(result, Is.EqualTo("True"));
        }

        [Test]
        public void BackupOnExit_SettingExists_SetValue()
        {
            this.context.Settings.Add(new() { Name = SettingHelper.BackupOnExitName, Value = "false" });
            this.context.SaveChanges();

            _ = new SettingHelper(this.context)
            {
                BackupOnExit = true,
            };

            var result = this.context.Settings.Single().Value;

            Assert.That(result, Is.EqualTo("True"));
        }
    }
}
