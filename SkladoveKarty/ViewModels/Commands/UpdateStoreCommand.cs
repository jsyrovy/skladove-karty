namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class UpdateStoreCommand : BaseCommand
    {
        public UpdateStoreCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(((Store)parameter).Name);
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.UpdateStore((Store)parameter);
            this.SettingsViewModel.LastActionStatus = "Sklad byl aktualizován.";
        }
    }
}
