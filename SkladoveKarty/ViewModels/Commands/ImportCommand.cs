﻿namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.IO;
    using System.Windows;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    public class ImportCommand : BaseCommand
    {
        public ImportCommand(AdministrationViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                if (!FileHelper.DirectoryExists(FileHelper.ImportDirectoryPath))
                    throw new DirectoryNotFoundException($"Složka '{FileHelper.ImportDirectoryPath}' neexistuje.");

                if (!FileHelper.FileExists(FileHelper.ImportItemsFilePath) && !FileHelper.FileExists(FileHelper.ImportSuppliersFilePath))
                    throw new FileNotFoundException("Nenalezen žádný soubor k importu.");

                var importHelper = new ImportHelper(this.AdministrationViewModel.Database);

                importHelper.ImportItems(
                    FileHelper.ReadCsv<ImportExportItem>(FileHelper.ImportItemsFilePath));

                importHelper.ImportSuppliers(
                    FileHelper.ReadCsv<ImportExportSupplier>(FileHelper.ImportSuppliersFilePath));

                this.AdministrationViewModel.Database.SaveChanges();

                this.AdministrationViewModel.LoadAllAsync();

                this.AdministrationViewModel.LastActionStatus = "Data byly importovány.";
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
