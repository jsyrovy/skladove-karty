﻿namespace SkladoveKarty.ViewModels.Commands
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
            this.ViewModel.Database.DeleteItem((Item)parameter);
            this.ViewModel.LoadItems();
            this.ViewModel.LastActionStatus = "Položka byla smazána.";
        }
    }
}
