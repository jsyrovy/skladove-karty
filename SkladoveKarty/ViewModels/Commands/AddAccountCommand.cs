namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddAccountCommand : BaseCommand
    {
        public AddAccountCommand(AdministrationViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            this.AdministrationViewModel.NewAccount.DateTime = DateTime.Now;
            this.AdministrationViewModel.DatabaseHelper.Add(this.AdministrationViewModel.NewAccount);
            this.AdministrationViewModel.DatabaseHelper.SaveChanges();
            this.AdministrationViewModel.LoadAccountsAsync(this.AdministrationViewModel.NewAccount);
            this.AdministrationViewModel.NewAccount = new();
            this.AdministrationViewModel.LastActionStatus = "Účet byl přidán.";
        }
    }
}
