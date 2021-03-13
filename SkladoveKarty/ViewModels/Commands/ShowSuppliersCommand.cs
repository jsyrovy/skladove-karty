namespace SkladoveKarty.ViewModels.Commands
{
    using System.Windows;
    using SkladoveKarty.Views;

    public class ShowSuppliersCommand : BaseCommand
    {
        public ShowSuppliersCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return this.MainViewModel.SelectedStorageCard != null;
        }

        public override void Execute(object parameter)
        {
            var window = new SuppliersWindow
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                DataContext = new SuppliersViewModel(this.MainViewModel.SelectedStorageCard),
            };

            window.ShowDialog();
        }
    }
}
