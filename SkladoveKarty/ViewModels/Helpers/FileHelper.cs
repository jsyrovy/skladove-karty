namespace SkladoveKarty.ViewModels.Helpers
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using CsvHelper;
    using CsvHelper.TypeConversion;

    public static class FileHelper
    {
        public static string ExportDirectoryPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "export");

        public static string ExportItemsPath => Path.Combine(ExportDirectoryPath, "items.csv");

        public static string ExportSuppliersPath => Path.Combine(ExportDirectoryPath, "suppliers.csv");

        public static void CreateExportDirectory()
        {
            Directory.CreateDirectory(ExportDirectoryPath);
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
