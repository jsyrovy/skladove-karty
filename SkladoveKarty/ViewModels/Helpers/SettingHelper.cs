namespace SkladoveKarty.ViewModels.Helpers
{
    using System;
    using System.Linq;
    using SkladoveKarty.Models;

    public class SettingHelper
    {
        public const string BackupDirectoryName = "BACKUP-DIRECTORY";
        public const string BackupOnExitName = "BACKUP-ON-EXIT";

        private readonly IDatabaseContext databaseContext;
        private static Lazy<SettingHelper> instance;
        private string backupDirectory;
        private bool backupOnExit;

        public SettingHelper(IDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;

            this.backupDirectory = this.GetValue<string>(BackupDirectoryName);
            this.backupOnExit = this.GetValue<bool>(BackupOnExitName);
        }

        public string BackupDirectory
        {
            get
            {
                return this.backupDirectory ?? FileHelper.DefaultBackupDirectoryPath;
            }

            set
            {
                this.backupDirectory = value;
                this.SaveValue(BackupDirectoryName, value);
            }
        }

        public bool BackupOnExit
        {
            get
            {
                return this.backupOnExit;
            }

            set
            {
                this.backupOnExit = value;
                this.SaveValue(BackupOnExitName, value);
            }
        }

        public static SettingHelper GetInstance(IDatabaseContext databaseContext)
        {
            if (instance == null)
                instance = new Lazy<SettingHelper>(() => new SettingHelper(databaseContext));

            return instance.Value;
        }

        private T GetValue<T>(string name)
        {
            var setting = this.GetSetting(name);

            if (setting == null) return default;

            return (T)Convert.ChangeType(setting.Value, typeof(T));
        }

        private Setting GetSetting(string name)
        {
            return this.databaseContext.Settings.Where(s => s.Name == name).SingleOrDefault();
        }

        private void SaveValue<T>(string name, T value)
        {
            var setting = this.GetSetting(name);

            if (setting != null)
            {
                setting.Value = value.ToString();
            }
            else
            {
                setting = new()
                {
                    Name = name,
                    Value = value.ToString(),
                };

                this.databaseContext.Settings.Add(setting);
            }

            this.databaseContext.SaveChanges();
        }
    }
}
