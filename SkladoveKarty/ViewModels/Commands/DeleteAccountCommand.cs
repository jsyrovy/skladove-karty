namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.Models;

    public class DeleteAccountCommand : BaseCommand
    {
        public DeleteAccountCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                this.SettingsViewModel.Database.DeleteAccount((Account)parameter);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.SettingsViewModel.LoadAccountsAsync();
            this.SettingsViewModel.LastActionStatus = "Účet byl smazán.";
        }
    }
}
