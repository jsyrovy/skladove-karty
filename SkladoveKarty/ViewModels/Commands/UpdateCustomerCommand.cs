namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class UpdateCustomerCommand : BaseCommand
    {
        public UpdateCustomerCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(((Customer)parameter).Name);
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.UpdateCustomer((Customer)parameter);
            this.SettingsViewModel.LastActionStatus = "Zákazník byl aktualizován.";
        }
    }
}
