namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class DeleteSupplierCommand : BaseCommand
    {
        public DeleteSupplierCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.DeleteSupplier((Supplier)parameter);
            this.SettingsViewModel.LoadSuppliersAsync();
            this.SettingsViewModel.LastActionStatus = "Dodavatel byl smazán.";
        }
    }
}
