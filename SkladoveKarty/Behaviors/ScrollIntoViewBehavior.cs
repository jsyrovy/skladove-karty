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
            if (e.AddedItems.Count == 1 && e.AddedItems[0] is Item)
            {
                this.AssociatedObject.ScrollIntoView(this.AssociatedObject.SelectedItem);
            }
        }
    }
}
