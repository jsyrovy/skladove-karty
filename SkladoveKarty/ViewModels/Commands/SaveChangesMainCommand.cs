namespace SkladoveKarty.ViewModels.Commands
{
    public class SaveChangesMainCommand : BaseCommand
    {
        public SaveChangesMainCommand(MainViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.MainViewModel.Database.SaveChanges();
            this.MainViewModel.CalculateStorageCardReports();
            this.MainViewModel.LastActionStatus = "Položky byla aktualizovány.";
        }
    }
}
