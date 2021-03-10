namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class UpdateSupplierCommand : BaseCommand
    {
        public UpdateSupplierCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(((Supplier)parameter).Name);
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.UpdateSupplier((Supplier)parameter);
            this.SettingsViewModel.LastActionStatus = "Dodavatel byl aktualizován.";
        }
    }
}
