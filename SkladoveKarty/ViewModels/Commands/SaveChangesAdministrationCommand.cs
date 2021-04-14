namespace SkladoveKarty.ViewModels.Commands
{
    public class SaveChangesAdministrationCommand : BaseCommand
    {
        public SaveChangesAdministrationCommand(AdministrationViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            this.AdministrationViewModel.Database.SaveChanges();
            this.AdministrationViewModel.LastActionStatus = "Položky byly aktualizovány.";
        }
    }
}
