namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using SkladoveKarty.Models;

    public class CreateStorageCardCommand : BaseCommand
    {
        public CreateStorageCardCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.MainViewModel.SelectedStorageCard = new StorageCard
            {
                DateTime = DateTime.Now,
            };
            this.MainViewModel.LastActionStatus = "Skladová karta byla vytvořena.";
        }
    }
}
