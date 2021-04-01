namespace SkladoveKarty.ViewModels.Commands
{
    public class SaveChangesMainCommand : BaseCommand
    {
        public SaveChangesMainCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(this.MainViewModel.SelectedStorageCard?.Name)
                && this.MainViewModel.SelectedStorageCard?.Account != null
                && this.MainViewModel.SelectedStorageCard?.Category != null
                && this.MainViewModel.SelectedStorageCard?.Store != null;
        }

        public override void Execute(object parameter)
        {
            if (this.MainViewModel.SelectedStorageCard?.Id == 0)
            {
                this.MainViewModel.Database.AddAndSave(this.MainViewModel.SelectedStorageCard);
                this.MainViewModel.SelectedStorageCard = null;
                this.MainViewModel.LastActionStatus = "Skladová karta byla přidána.";
                this.MainViewModel.LoadAllAsync();
                return;
            }

            this.MainViewModel.Database.SaveChanges();
            this.MainViewModel.CalculateStorageCardReports();
            this.MainViewModel.LastActionStatus = "Položky byla aktualizovány.";
        }
    }
}
