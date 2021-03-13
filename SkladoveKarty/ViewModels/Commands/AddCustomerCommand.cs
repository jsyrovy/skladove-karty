namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddCustomerCommand : BaseCommand
    {
        public AddCustomerCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.NewCustomer.DateTime = DateTime.Now;
            this.SettingsViewModel.Database.Add(this.SettingsViewModel.NewCustomer);
            this.SettingsViewModel.LoadCustomersAsync(this.SettingsViewModel.NewCustomer);
            this.SettingsViewModel.NewCustomer = new();
            this.SettingsViewModel.LastActionStatus = "Zákazník byl přidán.";
        }
    }
}
