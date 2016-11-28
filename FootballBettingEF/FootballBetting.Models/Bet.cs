namespace FootballBetting.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Bet
    {
        public Bet()
        {
            this.BetGames = new HashSet<BetGame>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BetId { get; set; }

        [Required]
        public decimal BetMoney { get; set; }

        public DateTime? DateTimeOfBet { get; set; }

        public virtual int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<BetGame> BetGames { get; set; }
    }
}
