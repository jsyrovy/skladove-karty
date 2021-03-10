namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class DeleteCategoryCommand : BaseCommand
    {
        public DeleteCategoryCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.DeleteCategory((Category)parameter);
            this.SettingsViewModel.LoadCategoriesAsync();
            this.SettingsViewModel.LastActionStatus = "Kategorie byla smazána.";
        }
    }
}
