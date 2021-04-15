namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Helpers;

    public class DeleteStorageCardCommand : BaseCommand
    {
        public DeleteStorageCardCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return this.MainViewModel.SelectedStorageCard != null
                && this.MainViewModel.SelectedStorageCard.Id != 0;
        }

        public override void Execute(object parameter)
        {
            var storageCard = (StorageCard)parameter;
            var result = MessageBox.Show(
                $"Opravdu chcete smazat skladovou kartu '{storageCard.Name}' a všechny její položky?",
                "Smazání skladové karty",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                this.MainViewModel.DatabaseHelper.DeleteStorageCard(storageCard);
                this.MainViewModel.DatabaseHelper.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.MainViewModel.LoadAllAsync();
            this.MainViewModel.LastActionStatus = "Skladová karta byla smazána.";
        }
    }
}
