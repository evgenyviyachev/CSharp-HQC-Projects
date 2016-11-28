namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CompetitionType
    {
        public CompetitionType()
        {
            this.Competitions = new HashSet<Competition>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompetitionTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Competition> Competitions { get; set; }
    }
}
