namespace SkladoveKarty.ViewModels.Helpers
{
    using System.Linq;
    using SkladoveKarty.Models;

    public static class ReportHelper
    {
        public static decimal GetStorageCardItemsPrice(StorageCard storageCard, int movement)
        {
            return storageCard.Items.Where(i => i.Movement == movement).Select(i => i.Price * i.Qty).Sum();
        }

        public static decimal GetStorageCardItemsPrice(StorageCard storageCard)
        {
            var averageIncomingPrice = storageCard.Items.Where(i => i.Movement == 1).Select(i => i.Price).Average();
            var outgoingPrice = storageCard.Items.Where(i => i.Movement == -1).Select(i => averageIncomingPrice * i.Qty).Sum();
            var incomingPrice = GetStorageCardItemsPrice(storageCard, 1);

            return incomingPrice - outgoingPrice;
        }

        public static int GetStorageCardItemsQty(StorageCard storageCard)
        {
            return storageCard.Items.Select(i => i.Qty * i.Movement).Sum();
        }
    }
}
