namespace SkladoveKarty.Views
{
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.DataContext is IClosing dataContext)
            {
                e.Cancel = !dataContext.OnClosing();
            }
        }
    }
}
