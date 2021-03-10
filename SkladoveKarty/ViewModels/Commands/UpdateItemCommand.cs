namespace SkladoveKarty.ViewModels.Commands
{
    using SkladoveKarty.Models;

    public class UpdateItemCommand : BaseCommand
    {
        public UpdateItemCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            var item = (Item)parameter;

            return !string.IsNullOrWhiteSpace(item.Name)
                && item.Price > 0
                && item.Qty > 0;
        }

        public override void Execute(object parameter)
        {
            this.MainViewModel.Database.UpdateItem((Item)parameter);
            this.MainViewModel.CalculateStorageCardReports();
            this.MainViewModel.LastActionStatus = "Položka byla aktualizována.";
        }
    }
}
