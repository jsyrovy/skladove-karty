namespace SkladoveKarty.ViewModels.Commands
{
    public class AddItemCommand : BaseCommand
    {
        public AddItemCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return this.MainViewModel.SelectedStorageCard != null
                && !string.IsNullOrWhiteSpace(this.MainViewModel.NewItem.Name)
                && this.MainViewModel.NewItem.Price > 0
                && this.MainViewModel.NewItem.Qty > 0;
        }

        public override void Execute(object parameter)
        {
            this.MainViewModel.NewItem.StorageCard = this.MainViewModel.SelectedStorageCard;
            this.MainViewModel.DatabaseHelper.Add(this.MainViewModel.NewItem);
            this.MainViewModel.DatabaseHelper.SaveChanges();
            this.MainViewModel.LoadItemsAsync(this.MainViewModel.NewItem);
            this.MainViewModel.CalculateStorageCardReports();
            this.MainViewModel.NewItem = MainViewModel.CreateDefaultItem();
            this.MainViewModel.LastActionStatus = "Položka byla přidána.";
        }
    }
}
