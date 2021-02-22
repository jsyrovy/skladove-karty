namespace SkladoveKarty.ViewModels.Commands
{
    using System;
    using System.Windows.Input;

    public abstract class BaseCommand : ICommand
    {
        public BaseCommand(MainViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        protected MainViewModel ViewModel { get; private set; }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);
    }
}
