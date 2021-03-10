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
            this.MainViewModel.Database.UpdateStorageCard(this.MainViewModel.SelectedStorageCard);
            this.MainViewModel.LastActionStatus = "Skladová karta byla aktualizována.";
        }
    }
}
