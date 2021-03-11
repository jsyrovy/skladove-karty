namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.Models;

    public class DeleteCategoryCommand : BaseCommand
    {
        public DeleteCategoryCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                this.SettingsViewModel.Database.DeleteCategory((Category)parameter);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.SettingsViewModel.LoadCategoriesAsync();
            this.SettingsViewModel.LastActionStatus = "Kategorie byla smazána.";
        }
    }
}
