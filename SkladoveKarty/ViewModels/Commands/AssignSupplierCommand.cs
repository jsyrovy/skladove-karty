namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using SkladoveKarty.Models;

    public class AssignSupplierCommand : BaseCommand
    {
        public AssignSupplierCommand(SuppliersViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            var storageCardSupplier = new StorageCardSupplier
            {
                DateTime = DateTime.Now,
                StorageCard = this.SuppliersViewModel.DatabaseHelper.GetStorageCard(this.SuppliersViewModel.SelectedStorageCard.Id),
                Supplier = (Supplier)parameter,
            };

            this.SuppliersViewModel.DatabaseHelper.Add(storageCardSupplier);
            this.SuppliersViewModel.DatabaseHelper.SaveChanges();
            this.SuppliersViewModel.LoadSuppliersAsync();
            this.SuppliersViewModel.LastActionStatus = "Dodavatel byl vybrán.";
        }
    }
}
