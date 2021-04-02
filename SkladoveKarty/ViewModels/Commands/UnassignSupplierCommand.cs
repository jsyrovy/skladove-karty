namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using SkladoveKarty.Models;

    public class UnassignSupplierCommand : BaseCommand
    {
        public UnassignSupplierCommand(SuppliersViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            var storageCardSupplier = this.SuppliersViewModel.Database.GetStorageCardSupplier(
                this.SuppliersViewModel.SelectedStorageCard, (Supplier)parameter);
            this.SuppliersViewModel.Database.DeleteStorageCardSupplier(storageCardSupplier);
            this.SuppliersViewModel.Database.SaveChanges();
            this.SuppliersViewModel.LoadSuppliersAsync();
            this.SuppliersViewModel.LastActionStatus = "Dodavatel byl odebrán.";
        }
    }
}
