namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.Models;

    public class DeleteStoreCommand : BaseCommand
    {
        public DeleteStoreCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                this.SettingsViewModel.Database.DeleteStore((Store)parameter);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.SettingsViewModel.LoadStoresAsync();
            this.SettingsViewModel.LastActionStatus = "Sklad byl smazán.";
        }
    }
}
