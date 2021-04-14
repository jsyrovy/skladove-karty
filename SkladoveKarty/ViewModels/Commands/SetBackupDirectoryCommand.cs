namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.ViewModels.Helpers;

    public class SetBackupDirectoryCommand : BaseCommand
    {
        public SetBackupDirectoryCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                    this.SettingsViewModel.BackupDirectory = dialog.SelectedPath;
                    this.SettingsViewModel.LastActionStatus = $"Umístění záloh bylo změněno na '{dialog.SelectedPath}'.";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
