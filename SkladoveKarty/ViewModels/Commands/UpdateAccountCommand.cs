namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class UpdateAccountCommand : BaseCommand
    {
        public UpdateAccountCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(((Account)parameter).Name);
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.UpdateAccount((Account)parameter);
            this.SettingsViewModel.LastActionStatus = "Účet byl aktualizován.";
        }
    }
}
