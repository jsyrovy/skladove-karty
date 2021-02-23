namespace SkladoveKarty.Behaviors
{
    using System.Collections.Specialized;
    using System.Windows.Controls;
    using Microsoft.Xaml.Behaviors;

    public class ScrollIntoViewBehavior : Behavior<ListView>
    {
        protected override void OnAttached()
        {
            var listBox = this.AssociatedObject;
            ((INotifyCollectionChanged)listBox.Items).CollectionChanged += this.OnCollectionChanged;
        }

        protected override void OnDetaching()
        {
            var listBox = this.AssociatedObject;
            ((INotifyCollectionChanged)listBox.Items).CollectionChanged -= this.OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var listView = this.AssociatedObject;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                listView.ScrollIntoView(e.NewItems[0]);
            }
        }
    }
}
