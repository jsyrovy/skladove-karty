namespace SkladoveKarty.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Setting
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
