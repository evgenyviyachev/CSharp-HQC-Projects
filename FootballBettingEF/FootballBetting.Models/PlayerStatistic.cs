namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PlayerStatistic
    {
        [Key, Column(Order = 1)]
        public virtual int PlayerId { get; set; }

        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        [Key, Column(Order = 2)]
        public virtual int GameId { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        public int ScoredGoals { get; set; }

        public int PlayerAssists { get; set; }

        public int PlayedMinutesDuringGame { get; set; }
    }
}
