namespace FootballBetting.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Game
    {
        public Game()
        {
            this.Statistics = new HashSet<PlayerStatistic>();
            this.BetGames = new HashSet<BetGame>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }

        //Fluent API
        //[Required]
        //public virtual int HomeTeamId { get; set; }

        //[ForeignKey("HomeTeamId")]
        public virtual Team HomeTeam { get; set; }

        //Have to switch off cascadeDelete -> Fluent API
        //[Required]
        //public virtual int AwayTeamId { get; set; }
        
        //[ForeignKey("AwayTeamId")]
        public virtual Team AwayTeam { get; set; }

        [Required]
        public int HomeGoals { get; set; }

        [Required]
        public int AwayGoals { get; set; }

        public DateTime? DateTimeOfGame { get; set; }

        [Required]
        public double HomeTeamBetRate { get; set; }

        [Required]
        public double AwayTeamBetRate { get; set; }

        [Required]
        public double DrawBetRate { get; set; }

        public virtual int CompetitionId { get; set; }

        [ForeignKey("CompetitionId")]
        public virtual Competition Competition { get; set; }

        public virtual int RoundId { get; set; }

        [ForeignKey("RoundId")]
        public virtual Round Round { get; set; }

        public virtual ICollection<PlayerStatistic> Statistics { get; set; }

        public virtual ICollection<BetGame> BetGames { get; set; }
    }
}
