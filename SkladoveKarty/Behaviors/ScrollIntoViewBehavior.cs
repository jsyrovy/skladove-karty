namespace SkladoveKarty.Behaviors
{
    using System.Windows.Controls;
    using Microsoft.Xaml.Behaviors;
    using SkladoveKarty.Models;

    public class ScrollIntoViewBehavior : Behavior<ListView>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.SelectionChanged += this.OnSelectionChanged;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.SelectionChanged -= this.OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1
                && (e.AddedItems[0] is Account
                    || e.AddedItems[0] is Category
                    || e.AddedItems[0] is Customer
                    || e.AddedItems[0] is Item
                    || e.AddedItems[0] is Store
                    || e.AddedItems[0] is Supplier))
            {
                this.AssociatedObject.ScrollIntoView(this.AssociatedObject.SelectedItem);
            }
        }
    }
}
