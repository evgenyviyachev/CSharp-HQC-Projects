namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.HomeTeamsGame = new HashSet<Game>();
            this.AwayTeamsGame = new HashSet<Game>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] Logo { get; set; }

        [Required]
        [MaxLength(3), MinLength(3)]
        public string Initials { get; set; }

        public decimal Budget { get; set; }

        [Required]
        public virtual int TownId { get; set; }

        [ForeignKey("TownId")]
        public virtual Town Town { get; set; }

        //Fluent API
        //[Required]
        //public virtual int PrimaryKitColorId { get; set; }

        //[ForeignKey("PrimaryKitColorId")]
        public virtual Color PrimaryKitColor { get; set; }

        //Fluent API
        //[Required]
        //public virtual int SecondaryKitColorId { get; set; }

        //[ForeignKey("SecondaryKitColorId")]
        public virtual Color SecondaryKitColor { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        [InverseProperty("HomeTeam")]
        public virtual ICollection<Game> HomeTeamsGame { get; set; }

        [InverseProperty("AwayTeam")]
        public virtual ICollection<Game> AwayTeamsGame { get; set; }
    }
}
