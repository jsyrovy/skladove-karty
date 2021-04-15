namespace SkladoveKarty.ViewModels.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.TypeConversion;

    public static class FileHelper
    {
        private const string ItemsFileName = "items.csv";
        private const string SuppliersFileName = "suppliers.csv";
        private const string StorageCardTemplateFileName = "StorageCard.Mustache";
        private const string DateTimeFormat = "yyyy-MM-dd-HH-mm-ss";

        public static string ImportDirectoryPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "import");

        public static string ImportItemsFilePath => Path.Combine(ImportDirectoryPath, ItemsFileName);

        public static string ImportSuppliersFilePath => Path.Combine(ImportDirectoryPath, SuppliersFileName);

        public static string ExportDirectoryPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "export");

        public static string ExportItemsFilePath => Path.Combine(ExportDirectoryPath, ItemsFileName);

        public static string ExportSuppliersFilePath => Path.Combine(ExportDirectoryPath, SuppliersFileName);

        public static string TemplatesDirectoryPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");

        public static string StorageCardTemplateFilePath => Path.Combine(TemplatesDirectoryPath, StorageCardTemplateFileName);

        public static string DefaultBackupDirectoryPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup");

        public static string GetExportStarageCardFilePath(string storageCardName)
        {
            return Path.Combine(Path.GetTempPath(), $"{GetValidFileName(storageCardName)}_{DateTime.Now.ToString(DateTimeFormat)}.html");
        }

        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static List<T> ReadCsv<T>(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<T>().ToList();
            }
        }

        public static void WriteCsv(string path, IEnumerable records)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                var options = new TypeConverterOptions { Formats = new[] { "yyyy-MM-dd" } };
                csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);
                csv.WriteRecords(records);
            }
        }

        public static string ReadText(string path)
        {
            return File.ReadAllText(path);
        }

        public static void WriteText(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public static void Open(string path)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = true,
                },
            };

            process.Start();
        }

        public static string GetValidFileName(string name)
        {
            var invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (var invalidChar in invalidChars)
                name = name.Replace(invalidChar.ToString(), string.Empty);

            return name;
        }

        public static string GetCurrentBackupDirectoryPath(string backupDirectoryPath = null)
        {
            return Path.Combine(backupDirectoryPath ?? DefaultBackupDirectoryPath, DateTime.Now.ToString(DateTimeFormat));
        }

        public static string GetItemsBackupPath(string backupDirectoryPath = null)
        {
            return Path.Combine(backupDirectoryPath ?? DefaultBackupDirectoryPath, ItemsFileName);
        }

        public static string GetSuppliersBackupPath(string backupDirectoryPath = null)
        {
            return Path.Combine(backupDirectoryPath ?? DefaultBackupDirectoryPath, SuppliersFileName);
        }
    }
}
