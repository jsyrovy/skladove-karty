namespace SkladoveKarty.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class StorageCardSupplier
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public StorageCard StorageCard { get; set; }

        [Required]
        public Supplier Supplier { get; set; }
    }
}
