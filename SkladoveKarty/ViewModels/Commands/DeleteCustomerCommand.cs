namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.Models;

    public class DeleteCustomerCommand : BaseCommand
    {
        public DeleteCustomerCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                this.SettingsViewModel.Database.DeleteCustomer((Customer)parameter);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.SettingsViewModel.LoadCustomersAsync();
            this.SettingsViewModel.LastActionStatus = "Zákazník byl smazán.";
        }
    }
}
