namespace SkladoveKarty.ViewModels.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using SkladoveKarty.Models;

    public static class ExportHelper
    {
        public static List<ImportExportItem> GetExportItems(List<StorageCard> storageCards)
        {
            var exportItems = new List<ImportExportItem>();

            foreach (var storageCard in storageCards)
            {
                foreach (var item in storageCard.Items)
                {
                    exportItems.Add(new ImportExportItem
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
                        ItemCustomerName = item.Customer?.Name,
                    });
                }
            }

            return exportItems.OrderBy(e => e.StorageCardName).ThenBy(e => e.ItemDateTime).ThenBy(e => e.ItemName).ToList();
        }

        public static List<ImportExportSupplier> GetExportSuppliers(List<StorageCardSupplier> storageCardSuppliers)
        {
            var exportSuppliers = new List<ImportExportSupplier>();

            foreach (var storageCardSupplier in storageCardSuppliers)
            {
                exportSuppliers.Add(new ImportExportSupplier
                {
                    StorageCardName = storageCardSupplier.StorageCard.Name,
                    SupplierName = storageCardSupplier.Supplier.Name,
                });
            }

            return exportSuppliers.OrderBy(e => e.StorageCardName).ThenBy(e => e.SupplierName).ToList();
        }
    }
}