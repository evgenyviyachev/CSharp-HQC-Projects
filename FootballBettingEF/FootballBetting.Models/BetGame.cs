namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BetGame
    {
        [Key, Column(Order = 1)]
        public virtual int BetId { get; set; }

        [ForeignKey("BetId")]
        public virtual Bet Bet { get; set; }

        [Key, Column(Order = 2)]
        public virtual int GameId { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [Required]
        public virtual int PredictionId { get; set; }

        [ForeignKey("PredictionId")]
        public virtual ResultPrediction Prediction { get; set; }
    }
}
