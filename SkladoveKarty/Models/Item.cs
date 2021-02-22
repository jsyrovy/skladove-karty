namespace SkladoveKarty.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Movement { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Invoice { get; set; }

        [Required]
        public StorageCard StorageCard { get; set; }

        public Customer Customer { get; set; }
    }
}
