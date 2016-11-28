namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Country
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
            this.Continents = new HashSet<Continent>();
        }

        [Key]
        [MinLength(3), MaxLength(3)]
        public string CountryId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public virtual ICollection<Continent> Continents { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
