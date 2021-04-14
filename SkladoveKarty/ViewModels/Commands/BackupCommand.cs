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
                FileHelper.CreateDirectory(this.SettingsViewModel.BackupDirectory);

                FileHelper.WriteCsv(
                    FileHelper.GetFilePathWithTimestamp("items", ".csv", this.SettingsViewModel.BackupDirectory),
                    ExportHelper.GetExportItems(this.SettingsViewModel.Database.GetStorageCards()));

                FileHelper.WriteCsv(
                    FileHelper.GetFilePathWithTimestamp("suppliers", ".csv", this.SettingsViewModel.BackupDirectory),
                    ExportHelper.GetExportSuppliers(this.SettingsViewModel.Database.GetStorageCardSuppliers()));

                this.SettingsViewModel.LastActionStatus = $"Data byly zálohovány do '{this.SettingsViewModel.BackupDirectory}'.";
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
