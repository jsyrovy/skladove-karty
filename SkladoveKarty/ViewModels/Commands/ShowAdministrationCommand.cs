namespace SkladoveKarty.ViewModels.Commands
{
    using System.Windows;
    using SkladoveKarty.Views;

    public class ShowAdministrationCommand : BaseCommand
    {
        public ShowAdministrationCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            var window = new AdministrationWindow
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            window.ShowDialog();

            this.MainViewModel.LoadAllAsync();
        }
    }
}
