namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.Models;

    public class DeleteSupplierCommand : BaseCommand
    {
        public DeleteSupplierCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                this.SettingsViewModel.Database.DeleteSupplier((Supplier)parameter);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.SettingsViewModel.LoadSuppliersAsync();
            this.SettingsViewModel.LastActionStatus = "Dodavatel byl smazán.";
        }
    }
}
