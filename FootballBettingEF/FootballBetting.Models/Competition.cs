namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Competition
    {
        public Competition()
        {
            this.Games = new HashSet<Game>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompetitionId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual int CompetitionTypeId { get; set; }

        [ForeignKey("CompetitionTypeId")]
        public virtual CompetitionType CompetitionType { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
