namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddSupplierCommand : BaseCommand
    {
        public AddSupplierCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.NewSupplier.DateTime = DateTime.Now;
            this.SettingsViewModel.Database.AddSupplier(this.SettingsViewModel.NewSupplier);
            this.SettingsViewModel.LoadSuppliersAsync(this.SettingsViewModel.NewSupplier);
            this.SettingsViewModel.NewSupplier = new();
            this.SettingsViewModel.LastActionStatus = "Sodavatel byl přidán.";
        }
    }
}
