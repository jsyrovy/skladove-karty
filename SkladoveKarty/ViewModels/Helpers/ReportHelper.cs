namespace SkladoveKarty.ViewModels.Helpers
{
    using System.Linq;
    using SkladoveKarty.Models;

    public static class ReportHelper
    {
        public static decimal GetStorageCardPrice(StorageCard storageCard)
        {
            var averageIncomingPrice = storageCard.Items.Where(i => i.Movement == 1).Select(i => i.Price).Average();
            var incomingPrice = storageCard.Items.Where(i => i.Movement == 1).Select(i => i.Price * i.Qty).Sum();
            var outgoingPrice = storageCard.Items.Where(i => i.Movement == -1).Select(i => averageIncomingPrice * i.Qty).Sum();

            return decimal.Round(incomingPrice - outgoingPrice, 2);
        }
    }
}
