namespace SkladoveKarty.ViewModels.Commands
{
    using System.Windows;
    using SkladoveKarty.Views;

    public class ShowSettingsCommand : BaseCommand
    {
        public ShowSettingsCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            var window = new SettingsWindow
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            window.ShowDialog();

            this.MainViewModel.LoadAllAsync();
        }
    }
}
