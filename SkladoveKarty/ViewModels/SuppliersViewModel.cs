namespace SkladoveKarty.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using SkladoveKarty.Models;
    using SkladoveKarty.ViewModels.Commands;
    using SkladoveKarty.ViewModels.Helpers;

    public class SuppliersViewModel : INotifyPropertyChanged
    {
        private string lastActionStatus;

        public SuppliersViewModel(StorageCard selectedStorageCard)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            this.SelectedStorageCard = selectedStorageCard;

            this.AssignSupplierCommand = new(this);
            this.UnassignSupplierCommand = new(this);

            this.LoadSuppliersAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DatabaseHelper Database { get; private set; } = new();

        public ObservableCollection<Supplier> AvailableSuppliers { get; set; } = new();

        public ObservableCollection<Supplier> AssignedSuppliers { get; set; } = new();

        public AssignSupplierCommand AssignSupplierCommand { get; set; }

        public UnassignSupplierCommand UnassignSupplierCommand { get; set; }

        public string LastActionStatus
        {
            get
            {
                return this.lastActionStatus;
            }

            set
            {
                this.lastActionStatus = value;
                this.OnPropertyChanged(nameof(this.LastActionStatus));
            }
        }

        public StorageCard SelectedStorageCard { get; }

        public async void LoadSuppliersAsync()
        {
            await this.LoadAssignedSuppliersTask();
            await this.LoadAvailableSuppliersTask();
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Task LoadAssignedSuppliersTask()
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.AssignedSuppliers.Clear();

                    foreach (var storageCardSupplier in this.Database.GetStorageCardSuppliers(this.SelectedStorageCard))
                        this.AssignedSuppliers.Add(storageCardSupplier.Supplier);
                });
            });
        }

        private Task LoadAvailableSuppliersTask()
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.AvailableSuppliers.Clear();

                    foreach (var supplier in this.Database.GetSuppliers())
                    {
                        if (!this.AssignedSuppliers.Where(a => a.Id == supplier.Id).Any())
                            this.AvailableSuppliers.Add(supplier);
                    }
                });
            });
        }
    }
}
