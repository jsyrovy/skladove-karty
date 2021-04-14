namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddStoreCommand : BaseCommand
    {
        public AddStoreCommand(AdministrationViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            this.AdministrationViewModel.NewStore.DateTime = DateTime.Now;
            this.AdministrationViewModel.Database.Add(this.AdministrationViewModel.NewStore);
            this.AdministrationViewModel.Database.SaveChanges();
            this.AdministrationViewModel.LoadStoresAsync(this.AdministrationViewModel.NewStore);
            this.AdministrationViewModel.NewStore = new();
            this.AdministrationViewModel.LastActionStatus = "Sklad byl přidán.";
        }
    }
}
