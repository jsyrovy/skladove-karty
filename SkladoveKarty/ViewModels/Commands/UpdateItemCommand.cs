namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class UpdateItemCommand : BaseCommand
    {
        public UpdateItemCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.ViewModel.Database.UpdateItem((Item)parameter);
            this.ViewModel.CalculateStorageCardReports();
            this.ViewModel.LastActionStatus = "Položka byla aktualizována.";
        }
    }
}
