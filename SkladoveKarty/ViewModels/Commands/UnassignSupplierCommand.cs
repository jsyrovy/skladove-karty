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
            var storageCardSupplier = this.SuppliersViewModel.DatabaseHelper.GetStorageCardSupplier(
                this.SuppliersViewModel.SelectedStorageCard, (Supplier)parameter);
            this.SuppliersViewModel.DatabaseHelper.DeleteStorageCardSupplier(storageCardSupplier);
            this.SuppliersViewModel.DatabaseHelper.SaveChanges();
            this.SuppliersViewModel.LoadSuppliersAsync();
            this.SuppliersViewModel.LastActionStatus = "Dodavatel byl odebrán.";
        }
    }
}
