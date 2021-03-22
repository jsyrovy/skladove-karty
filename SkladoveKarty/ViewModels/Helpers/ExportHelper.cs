namespace SkladoveKarty.ViewModels.Helpers
{
    using System.Collections.Generic;
    using SkladoveKarty.Models;

    public static class ExportHelper
    {
        public static List<ExportItem> GetExportItems(List<StorageCard> storageCards)
        {
            var exportItems = new List<ExportItem>();

            foreach (var storageCard in storageCards)
            {
                foreach (var item in storageCard.Items)
                {
                    exportItems.Add(new ExportItem
                    {
                        StorageCardName = storageCard.Name,
                        AccountName = storageCard.Account.Name,
                        CategoryName = storageCard.Category.Name,
                        StoreName = storageCard.Store.Name,
                        ItemDateTime = item.DateTime,
                        ItemName = item.Name,
                        ItemMovement = item.Movement,
                        ItemQty = item.Qty,
                        ItemPrice = item.Price,
                        ItemInvoice = item.Invoice,
                    });
                }
            }

            return exportItems;
        }

        public static List<ExportSupplier> GetExportSuppliers(List<StorageCardSupplier> storageCardSuppliers)
        {
            var exportSuppliers = new List<ExportSupplier>();

            foreach (var storageCardSupplier in storageCardSuppliers)
            {
                exportSuppliers.Add(new ExportSupplier
                {
                    StorageCardName = storageCardSupplier.StorageCard.Name,
                    SupplierName = storageCardSupplier.Supplier.Name,
                });
            }

            return exportSuppliers;
        }
    }
}