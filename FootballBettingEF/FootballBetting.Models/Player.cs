namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Player
    {
        public Player()
        {
            this.Statistics = new HashSet<PlayerStatistic>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SquadNumber { get; set; }

        [Required]
        public bool IsCurrentlyInjured { get; set; }

        //Fluent API
        //public virtual int TeamId { get; set; }

        //[ForeignKey("TeamId")]
        public virtual Team Team { get; set; }

        [Required]
        [MaxLength(2), MinLength(2)]
        public virtual string PositionId { get; set; }

        [ForeignKey("PositionId")]
        public virtual Position Position { get; set; }

        public virtual ICollection<PlayerStatistic> Statistics { get; set; }
    }
}
