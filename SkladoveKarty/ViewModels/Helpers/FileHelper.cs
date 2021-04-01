namespace SkladoveKarty.ViewModels.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.TypeConversion;

    public static class FileHelper
    {
        private const string ItemsFileName = "items.csv";
        private const string SuppliersFileName = "suppliers.csv";

        public static string ImportDirectoryPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "import");

        public static string ImportItemsFilePath => Path.Combine(ImportDirectoryPath, ItemsFileName);

        public static string ImportSuppliersFilePath => Path.Combine(ImportDirectoryPath, SuppliersFileName);

        public static string ExportDirectoryPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "export");

        public static string ExportItemsFilePath => Path.Combine(ExportDirectoryPath, ItemsFileName);

        public static string ExportSuppliersFilePath => Path.Combine(ExportDirectoryPath, SuppliersFileName);

        public static void CreateExportDirectory()
        {
            Directory.CreateDirectory(ExportDirectoryPath);
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
    }
}
