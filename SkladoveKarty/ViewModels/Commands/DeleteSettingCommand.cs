namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    public class DeleteSettingCommand : BaseCommand
    {
        public DeleteSettingCommand(AdministrationViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                if (parameter is Account account)
                {
                    this.AdministrationViewModel.DatabaseHelper.DeleteAccount(account);
                    this.AdministrationViewModel.LoadAccountsAsync();
                    this.AdministrationViewModel.LastActionStatus = "Účet byl smazán.";
                }

                if (parameter is Category category)
                {
                    this.AdministrationViewModel.DatabaseHelper.DeleteCategory(category);
                    this.AdministrationViewModel.LoadCategoriesAsync();
                    this.AdministrationViewModel.LastActionStatus = "Kategorie byla smazána.";
                }

                if (parameter is Customer customer)
                {
                    this.AdministrationViewModel.DatabaseHelper.DeleteCustomer(customer);
                    this.AdministrationViewModel.LoadCustomersAsync();
                    this.AdministrationViewModel.LastActionStatus = "Zákazník byl smazán.";
                }

                if (parameter is Store store)
                {
                    this.AdministrationViewModel.DatabaseHelper.DeleteStore(store);
                    this.AdministrationViewModel.LoadStoresAsync();
                    this.AdministrationViewModel.LastActionStatus = "Sklad byl smazán.";
                }

                if (parameter is Supplier supplier)
                {
                    this.AdministrationViewModel.DatabaseHelper.DeleteSupplier(supplier);
                    this.AdministrationViewModel.LoadSuppliersAsync();
                    this.AdministrationViewModel.LastActionStatus = "Dodavatel byl smazán.";
                }

                this.AdministrationViewModel.DatabaseHelper.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
