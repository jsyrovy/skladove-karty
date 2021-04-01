namespace SkladoveKarty.Models
{
    using System;

    public class ImportExportItem
    {
        public string StorageCardName { get; set; }

        public string AccountName { get; set; }

        public string CategoryName { get; set; }

        public string StoreName { get; set; }

        public DateTime ItemDateTime { get; set; }

        public string ItemName { get; set; }

        public int ItemMovement { get; set; }

        public int ItemQty { get; set; }

        public decimal ItemPrice { get; set; }

        public string ItemInvoice { get; set; }

        public string ItemCustomerName { get; set; }
    }
}
