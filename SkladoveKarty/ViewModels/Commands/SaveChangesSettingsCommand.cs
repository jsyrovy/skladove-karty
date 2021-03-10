namespace SkladoveKarty.ViewModels.Commands
{
    public class SaveChangesSettingsCommand : BaseCommand
    {
        public SaveChangesSettingsCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.Database.SaveChanges();
            this.SettingsViewModel.LastActionStatus = "Nastavení bylo aktualizováno.";
        }
    }
}
