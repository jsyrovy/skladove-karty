namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddCustomerCommand : BaseCommand
    {
        public AddCustomerCommand(AdministrationViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            this.AdministrationViewModel.NewCustomer.DateTime = DateTime.Now;
            this.AdministrationViewModel.Database.Add(this.AdministrationViewModel.NewCustomer);
            this.AdministrationViewModel.Database.SaveChanges();
            this.AdministrationViewModel.LoadCustomersAsync(this.AdministrationViewModel.NewCustomer);
            this.AdministrationViewModel.NewCustomer = new();
            this.AdministrationViewModel.LastActionStatus = "Zákazník byl přidán.";
        }
    }
}
