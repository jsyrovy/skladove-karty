namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.ViewModels.Helpers;

    public class ExportCommand : BaseCommand
    {
        public ExportCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                FileHelper.CreateDirectory(FileHelper.ExportDirectoryPath);

                FileHelper.WriteCsv(
                    FileHelper.ExportItemsFilePath,
                    ExportHelper.GetExportItems(this.SettingsViewModel.DatabaseHelper.GetStorageCards()));

                FileHelper.WriteCsv(
                    FileHelper.ExportSuppliersFilePath,
                    ExportHelper.GetExportSuppliers(this.SettingsViewModel.DatabaseHelper.GetStorageCardSuppliers()));

                this.SettingsViewModel.LastActionStatus = $"Data byly exportovány do '{FileHelper.ExportDirectoryPath}'.";
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
