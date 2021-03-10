namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class DeleteAccountCommand : BaseCommand
    {
        public DeleteAccountCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.DeleteAccount((Account)parameter);
            this.SettingsViewModel.LoadAccountsAsync();
            this.SettingsViewModel.LastActionStatus = "Účet byl smazán.";
        }
    }
}
