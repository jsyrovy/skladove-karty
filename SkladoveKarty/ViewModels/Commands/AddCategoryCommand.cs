namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddCategoryCommand : BaseCommand
    {
        public AddCategoryCommand(AdministrationViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            this.AdministrationViewModel.NewCategory.DateTime = DateTime.Now;
            this.AdministrationViewModel.DatabaseHelper.Add(this.AdministrationViewModel.NewCategory);
            this.AdministrationViewModel.DatabaseHelper.SaveChanges();
            this.AdministrationViewModel.LoadCategoriesAsync(this.AdministrationViewModel.NewCategory);
            this.AdministrationViewModel.NewCategory = new();
            this.AdministrationViewModel.LastActionStatus = "Kategorie byla přidána.";
        }
    }
}
