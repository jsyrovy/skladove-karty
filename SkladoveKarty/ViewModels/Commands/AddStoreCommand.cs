namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddStoreCommand : BaseCommand
    {
        public AddStoreCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.NewStore.DateTime = DateTime.Now;
            this.SettingsViewModel.Database.AddAndSave(this.SettingsViewModel.NewStore);
            this.SettingsViewModel.LoadStoresAsync(this.SettingsViewModel.NewStore);
            this.SettingsViewModel.NewStore = new();
            this.SettingsViewModel.LastActionStatus = "Sklad byl přidán.";
        }
    }
}
