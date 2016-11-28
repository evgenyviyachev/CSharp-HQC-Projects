namespace MassDefect.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Planet
    {
        public Planet()
        {
            this.HomePeople = new HashSet<Person>();
            this.OriginAnomalies = new HashSet<Anomaly>();
            this.TeleportAnomalies = new HashSet<Anomaly>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Star Sun { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Person> HomePeople { get; set; }

        public virtual ICollection<Anomaly> OriginAnomalies { get; set; }

        public virtual ICollection<Anomaly> TeleportAnomalies { get; set; }
    }
}
