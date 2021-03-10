namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class DeleteItemCommand : BaseCommand
    {
        public DeleteItemCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.MainViewModel.Database.DeleteItem((Item)parameter);
            this.MainViewModel.LoadItemsAsync();
            this.MainViewModel.CalculateStorageCardReports();
            this.MainViewModel.LastActionStatus = "Položka byla smazána.";
        }
    }
}
