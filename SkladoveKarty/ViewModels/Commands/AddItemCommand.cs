namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddItemCommand : BaseCommand
    {
        public AddItemCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.ViewModel.NewItem.DateTime = DateTime.Now;
            this.ViewModel.NewItem.StorageCard = this.ViewModel.SelectedStorageCard;
            this.ViewModel.Database.AddItem(this.ViewModel.NewItem);
            this.ViewModel.LoadItems();
            this.ViewModel.NewItem = new();
            this.ViewModel.LastActionStatus = "Položka byla přidána.";
        }
    }
}
