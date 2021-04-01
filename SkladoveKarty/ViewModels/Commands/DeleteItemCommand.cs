namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    public class DeleteItemCommand : BaseCommand
    {
        public DeleteItemCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                this.MainViewModel.Database.DeleteItem((Item)parameter);
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.MainViewModel.LoadItemsAsync();
            this.MainViewModel.CalculateStorageCardReports();
            this.MainViewModel.LastActionStatus = "Položka byla smazána.";
        }
    }
}
