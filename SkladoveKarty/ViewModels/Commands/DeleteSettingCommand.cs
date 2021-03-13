namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.Models;

    public class DeleteSettingCommand : BaseCommand
    {
        public DeleteSettingCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                if (parameter is Account account)
                {
                    this.SettingsViewModel.Database.DeleteAccount(account);
                    this.SettingsViewModel.LoadAccountsAsync();
                    this.SettingsViewModel.LastActionStatus = "Účet byl smazán.";
                }

                if (parameter is Category category)
                {
                    this.SettingsViewModel.Database.DeleteCategory(category);
                    this.SettingsViewModel.LoadCategoriesAsync();
                    this.SettingsViewModel.LastActionStatus = "Kategorie byla smazána.";
                }

                if (parameter is Customer customer)
                {
                    this.SettingsViewModel.Database.DeleteCustomer(customer);
                    this.SettingsViewModel.LoadCustomersAsync();
                    this.SettingsViewModel.LastActionStatus = "Zákazník byl smazán.";
                }

                if (parameter is Store store)
                {
                    this.SettingsViewModel.Database.DeleteStore(store);
                    this.SettingsViewModel.LoadStoresAsync();
                    this.SettingsViewModel.LastActionStatus = "Sklad byl smazán.";
                }

                if (parameter is Supplier supplier)
                {
                    this.SettingsViewModel.Database.DeleteSupplier(supplier);
                    this.SettingsViewModel.LoadSuppliersAsync();
                    this.SettingsViewModel.LastActionStatus = "Dodavatel byl smazán.";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
