namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.ViewModels.Helpers;

    public class ExportCommand : BaseCommand
    {
        public ExportCommand(AdministrationViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                FileHelper.CreateExportDirectory();

                FileHelper.WriteCsv(
                    FileHelper.ExportItemsFilePath,
                    ExportHelper.GetExportItems(this.AdministrationViewModel.Database.GetStorageCards()));

                FileHelper.WriteCsv(
                    FileHelper.ExportSuppliersFilePath,
                    ExportHelper.GetExportSuppliers(this.AdministrationViewModel.Database.GetStorageCardSuppliers()));

                this.AdministrationViewModel.LastActionStatus = $"Data byly exportovány do '{FileHelper.ExportDirectoryPath}'.";
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
