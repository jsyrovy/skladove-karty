namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class DeleteStoreCommand : BaseCommand
    {
        public DeleteStoreCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.DeleteStore((Store)parameter);
            this.SettingsViewModel.LoadStoresAsync();
            this.SettingsViewModel.LastActionStatus = "Sklad byl smazán.";
        }
    }
}
