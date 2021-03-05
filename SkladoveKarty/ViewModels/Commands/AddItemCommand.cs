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
            return this.ViewModel.SelectedStorageCard != null
                && !string.IsNullOrWhiteSpace(this.ViewModel.NewItem.Name)
                && this.ViewModel.NewItem.Price > 0
                && this.ViewModel.NewItem.Qty > 0;
        }

        public override void Execute(object parameter)
        {
            this.ViewModel.NewItem.StorageCard = this.ViewModel.SelectedStorageCard;
            this.ViewModel.Database.AddItem(this.ViewModel.NewItem);
            this.ViewModel.LoadItemsAsync(this.ViewModel.NewItem);
            this.ViewModel.CalculateStorageCardReports();
            this.ViewModel.NewItem = MainViewModel.CreateDefaultItem();
            this.ViewModel.LastActionStatus = "Položka byla přidána.";
        }
    }
}
