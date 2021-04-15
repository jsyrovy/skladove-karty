namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.ViewModels.Helpers;

    public class BackupCommand : BaseCommand
    {
        public BackupCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                var directory = FileHelper.GetCurrentBackupDirectoryPath(this.SettingsViewModel.BackupDirectory);

                FileHelper.CreateDirectory(directory);

                FileHelper.WriteCsv(
                    FileHelper.GetItemsBackupPath(directory),
                    ExportHelper.GetExportItems(this.SettingsViewModel.Database.GetStorageCards()));

                FileHelper.WriteCsv(
                    FileHelper.GetSuppliersBackupPath(directory),
                    ExportHelper.GetExportSuppliers(this.SettingsViewModel.Database.GetStorageCardSuppliers()));

                this.SettingsViewModel.LastActionStatus = $"Data byly zálohovány do '{directory}'.";
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
