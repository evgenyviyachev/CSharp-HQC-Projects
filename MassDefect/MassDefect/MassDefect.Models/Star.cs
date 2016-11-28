namespace MassDefect.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Star
    {
        public Star()
        {
            this.Planets = new HashSet<Planet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Planet> Planets { get; set; }
    }
}
