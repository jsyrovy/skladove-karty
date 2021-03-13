namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddAccountCommand : BaseCommand
    {
        public AddAccountCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.NewAccount.DateTime = DateTime.Now;
            this.SettingsViewModel.Database.Add(this.SettingsViewModel.NewAccount);
            this.SettingsViewModel.LoadAccountsAsync(this.SettingsViewModel.NewAccount);
            this.SettingsViewModel.NewAccount = new();
            this.SettingsViewModel.LastActionStatus = "Účet byl přidán.";
        }
    }
}
