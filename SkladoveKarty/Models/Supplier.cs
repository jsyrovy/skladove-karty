namespace SkladoveKarty
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Supplier
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        public List<StorageCardSupplier> StorageCardSuppliers { get; } = new List<StorageCardSupplier>();
    }
}
