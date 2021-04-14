namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.IO;
    using System.Windows;
    using SkladoveKarty.ViewModels.Helpers;

    public class GenerateHtmlCommand : BaseCommand
    {
        public GenerateHtmlCommand(MainViewModel viewModel)
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
            try
            {
                if (!FileHelper.FileExists(FileHelper.StorageCardTemplateFilePath))
                    throw new FileNotFoundException($"Šablona '{FileHelper.StorageCardTemplateFilePath}' nenalezena.");

                var template = FileHelper.ReadText(FileHelper.StorageCardTemplateFilePath);
                var html = HtmlHelper.Generate(template, this.MainViewModel.SelectedStorageCard);
                var path = FileHelper.GetFilePathWithTimestamp(this.MainViewModel.SelectedStorageCard.Name, ".html");

                FileHelper.WriteText(path, html);
                FileHelper.Open(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionHelper.GetCompleteExceptionMessage(e), "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
