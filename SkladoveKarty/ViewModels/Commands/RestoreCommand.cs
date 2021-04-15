namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    public class RestoreCommand : BaseCommand
    {
        public RestoreCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            var result = MessageBox.Show(
                $"Opravdu chcete obnovit zálohu '{parameter}'?{Environment.NewLine}Všechny novější změny budou smazány.",
                "Obnova zálohy",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                this.SettingsViewModel.DatabaseHelper.DeleteAll();

                var importHelper = new ImportHelper(this.SettingsViewModel.DatabaseHelper);

                importHelper.ImportItems(
                    FileHelper.ReadCsv<ImportExportItem>(
                        FileHelper.GetItemsBackupPath(this.SettingsViewModel.BackupDirectory, parameter.ToString())));

                importHelper.ImportSuppliers(
                    FileHelper.ReadCsv<ImportExportSupplier>(
                        FileHelper.GetSuppliersBackupPath(this.SettingsViewModel.BackupDirectory, parameter.ToString())));

                this.SettingsViewModel.DatabaseHelper.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.SettingsViewModel.LastActionStatus = $"Záloha '{parameter}' byla obnovena.";
        }
    }
}
