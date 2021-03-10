namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class UpdateCategoryCommand : BaseCommand
    {
        public UpdateCategoryCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(((Category)parameter).Name);
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.UpdateCategory((Category)parameter);
            this.SettingsViewModel.LastActionStatus = "Kategorie byla aktualizována.";
        }
    }
}
