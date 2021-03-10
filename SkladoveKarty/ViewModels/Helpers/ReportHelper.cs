namespace SkladoveKarty.ViewModels.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using SkladoveKarty.Models;

    public static class ReportHelper
    {
        public static decimal GetStorageCardItemsPrice(List<Item> items, int movement)
        {
            return items?.Where(i => i.Movement == movement).Select(i => i.Price * i.Qty).Sum() ?? 0;
        }

        public static decimal GetStorageCardItemsPrice(List<Item> items)
        {
            if (items == null) return 0;

            var averageIncomingPrice = items.Where(i => i.Movement == 1).Select(i => i.Price).Average();
            var outgoingPrice = items.Where(i => i.Movement == -1).Select(i => averageIncomingPrice * i.Qty).Sum();
            var incomingPrice = GetStorageCardItemsPrice(items, 1);

            return incomingPrice - outgoingPrice;
        }

        public static int GetStorageCardItemsQty(List<Item> items)
        {
            return items?.Select(i => i.Qty * i.Movement).Sum() ?? 0;
        }
    }
}
