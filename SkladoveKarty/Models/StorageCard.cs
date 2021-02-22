namespace SkladoveKarty.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StorageCard
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Account Account { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public Store Store { get; set; }

        public List<StorageCardSupplier> StorageCardSuppliers { get; } = new List<StorageCardSupplier>();

        public List<Item> Items { get; } = new List<Item>();
    }
}
