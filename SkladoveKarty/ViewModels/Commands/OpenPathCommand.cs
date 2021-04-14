namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows;
    using SkladoveKarty.ViewModels.Helpers;

    public class OpenPathCommand : BaseCommand
    {
        public OpenPathCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return FileHelper.FileExists(parameter?.ToString()) || FileHelper.DirectoryExists(parameter?.ToString());
        }

        public override void Execute(object parameter)
        {
            try
            {
                FileHelper.Open(parameter.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
