namespace SkladoveKarty
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        public List<StorageCard> StorageCards { get; } = new List<StorageCard>();
    }
}
