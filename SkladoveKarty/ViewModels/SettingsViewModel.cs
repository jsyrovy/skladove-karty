namespace SkladoveKarty.ViewModels
{
    using System.ComponentModel;
    using System.Windows;
    using SkladoveKarty.ViewModels.Commands;

    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            this.ImportCommand = new ImportCommand(this);
            this.ExportCommand = new ExportCommand(this);
        }

        public ImportCommand ImportCommand { get; set; }

        public ExportCommand ExportCommand { get; set; }
    }
}
