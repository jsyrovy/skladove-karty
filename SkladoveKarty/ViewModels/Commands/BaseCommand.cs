namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows.Input;

    public abstract class BaseCommand : ICommand
    {
        public BaseCommand(MainViewModel viewModel)
        {
            this.MainViewModel = viewModel;
        }

        public BaseCommand(AdministrationViewModel viewModel)
        {
            this.AdministrationViewModel = viewModel;
        }

        public BaseCommand(SuppliersViewModel viewModel)
        {
            this.SuppliersViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        protected MainViewModel MainViewModel { get; private set; }

        protected AdministrationViewModel AdministrationViewModel { get; private set; }

        protected SuppliersViewModel SuppliersViewModel { get; private set; }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);
    }
}
