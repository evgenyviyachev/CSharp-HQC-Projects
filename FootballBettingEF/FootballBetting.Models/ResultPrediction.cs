namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum PredictionType
    {
        HomeTeam,
        AwayTeam,
        Draw
    }

    public class ResultPrediction
    {
        public ResultPrediction()
        {
            this.BetGames = new HashSet<BetGame>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PredictionId { get; set; }

        [Required]
        public PredictionType Prediction { get; set; }

        public virtual ICollection<BetGame> BetGames { get; set; }
    }
}
