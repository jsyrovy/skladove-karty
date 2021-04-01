﻿namespace SkladoveKarty.ViewModels.Commands
{
    using System;

    public class AddCategoryCommand : BaseCommand
    {
        public AddCategoryCommand(SettingsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            this.SettingsViewModel.NewCategory.DateTime = DateTime.Now;
            this.SettingsViewModel.Database.AddAndSave(this.SettingsViewModel.NewCategory);
            this.SettingsViewModel.LoadCategoriesAsync(this.SettingsViewModel.NewCategory);
            this.SettingsViewModel.NewCategory = new();
            this.SettingsViewModel.LastActionStatus = "Kategorie byla přidána.";
        }
    }
}
