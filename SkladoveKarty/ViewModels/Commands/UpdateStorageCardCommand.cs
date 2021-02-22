namespace SkladoveKarty.ViewModels.Commands
{
    public class UpdateStorageCardCommand : BaseCommand
    {
        public UpdateStorageCardCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.ViewModel.Database.UpdateStorageCards(this.ViewModel.SelectedStorageCard);
            this.ViewModel.LastActionStatus = "Skladová karta byla aktualizována.";
        }
    }
}
