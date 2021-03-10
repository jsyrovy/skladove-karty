namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class DeleteCustomerCommand : BaseCommand
    {
        public DeleteCustomerCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.DeleteCustomer((Customer)parameter);
            this.SettingsViewModel.LoadCustomersAsync();
            this.SettingsViewModel.LastActionStatus = "Zákazník byl smazán.";
        }
    }
}
