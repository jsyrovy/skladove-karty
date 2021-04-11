namespace SkladoveKarty.ViewModels.Helpers
{
    using System;
    using System.Linq;
    using SkladoveKarty.Models;

    public static class SettingHelper
    {
        public static bool GetBoolValue(IDatabaseContext databaseContext, string name)
        {
            var setting = GetSetting(databaseContext, name);

            if (setting == null) return false;

            if (bool.TryParse(setting.Value, out bool result)) return result;

            throw new InvalidOperationException($"'{name}' value isn't boolean.");
        }

        public static void SaveValue<T>(IDatabaseContext databaseContext, string name, T value)
        {
            var setting = GetSetting(databaseContext, name);

            if (setting != null)
            {
                setting.Value = value.ToString();
                return;
            }

            setting = new()
            {
                Name = name,
                Value = value.ToString(),
            };

            databaseContext.Settings.Add(setting);
        }

        private static Setting GetSetting(IDatabaseContext databaseContext, string name)
        {
            return databaseContext.Settings.Where(s => s.Name == name).SingleOrDefault();
        }
    }
}
