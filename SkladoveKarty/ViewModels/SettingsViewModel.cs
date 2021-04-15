namespace SkladoveKarty.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Commands;
    using SkladoveKarty.ViewModels.Helpers;

    public class SettingsViewModel : BaseViewModel
    {
        private string backupDirectory;
        private bool backupOnExit;

        public SettingsViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            this.Settings = new SettingHelper(new DatabaseContext());

            this.ImportCommand = new ImportCommand(this);
            this.ExportCommand = new ExportCommand(this);

            this.BackupCommand = new BackupCommand(this);
            this.RestoreCommand = new RestoreCommand(this);
            this.SetBackupDirectoryCommand = new SetBackupDirectoryCommand(this);
            this.OpenPathCommand = new OpenPathCommand(this);

            this.backupDirectory = this.Settings.BackupDirectory;
            this.backupOnExit = this.Settings.BackupOnExit;

            this.LoadBackupDirectoriesAsync();
        }

        public SettingHelper Settings { get; private set; }

        public ObservableCollection<string> BackupDirectories { get; } = new();

        public string BackupDirectory
        {
            get
            {
                return this.backupDirectory;
            }

            set
            {
                this.backupDirectory = value;
                this.Settings.BackupDirectory = value;
                this.OnPropertyChanged(nameof(this.BackupDirectory));
            }
        }

        public bool BackupOnExit
        {
            get
            {
                return this.backupOnExit;
            }

            set
            {
                this.backupOnExit = value;
                this.Settings.BackupOnExit = value;
                this.OnPropertyChanged(nameof(this.BackupOnExit));
            }
        }

        public ImportCommand ImportCommand { get; set; }

        public ExportCommand ExportCommand { get; set; }

        public BackupCommand BackupCommand { get; set; }

        public RestoreCommand RestoreCommand { get; set; }

        public SetBackupDirectoryCommand SetBackupDirectoryCommand { get; set; }

        public OpenPathCommand OpenPathCommand { get; set; }

        public async void LoadBackupDirectoriesAsync()
        {
            await this.LoadBackupDirectoriesTask();
        }

        private Task LoadBackupDirectoriesTask()
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.BackupDirectories.Clear();

                    foreach (var directory in FileHelper.GetBackupDirectoriesPaths(this.Settings.BackupDirectory))
                        this.BackupDirectories.Add(directory);
                });
            });
        }
    }
}
