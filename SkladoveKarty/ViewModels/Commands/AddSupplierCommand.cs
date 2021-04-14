namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddSupplierCommand : BaseCommand
    {
        public AddSupplierCommand(AdministrationViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            this.AdministrationViewModel.NewSupplier.DateTime = DateTime.Now;
            this.AdministrationViewModel.Database.Add(this.AdministrationViewModel.NewSupplier);
            this.AdministrationViewModel.Database.SaveChanges();
            this.AdministrationViewModel.LoadSuppliersAsync(this.AdministrationViewModel.NewSupplier);
            this.AdministrationViewModel.NewSupplier = new();
            this.AdministrationViewModel.LastActionStatus = "Sodavatel byl přidán.";
        }
    }
}
