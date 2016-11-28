namespace FootballBetting.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
            : base("name=FootballBettingContext")
        {
        }

        public virtual IDbSet<Bet> Bets { get; set; }
        public virtual IDbSet<BetGame> BetGames { get; set; }
        public virtual IDbSet<Color> Colors { get; set; }
        public virtual IDbSet<Competition> Competitions { get; set; }
        public virtual IDbSet<CompetitionType> CompetitionTypes { get; set; }
        public virtual IDbSet<Continent> Continents { get; set; }
        public virtual IDbSet<Country> Countries { get; set; }
        public virtual IDbSet<Game> Games { get; set; }
        public virtual IDbSet<Player> Players { get; set; }
        public virtual IDbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public virtual IDbSet<Position> Positions { get; set; }
        public virtual IDbSet<ResultPrediction> ResultPredictions { get; set; }
        public virtual IDbSet<Round> Rounds { get; set; }
        public virtual IDbSet<Team> Teams { get; set; }
        public virtual IDbSet<Town> Towns { get; set; }
        public virtual IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasRequired(g => g.AwayTeam)
                .WithMany(t => t.AwayTeamsGame)
                .Map(tg =>
                {
                    tg.MapKey("AwayTeamId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasRequired(g => g.HomeTeam)
                .WithMany(t => t.HomeTeamsGame)
                .Map(tg =>
                {
                    tg.MapKey("HomeTeamId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasRequired(p => p.Team)
                .WithMany(t => t.Players)
                .Map(tp =>
                {
                    tp.MapKey("TeamId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasRequired(p => p.PrimaryKitColor)
                .WithMany(pkc => pkc.PrimaryKitTeams)
                .Map(pkcp =>
                {
                    pkcp.MapKey("PrimaryKitColorId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasRequired(p => p.SecondaryKitColor)
                .WithMany(skc => skc.SecondaryKitTeams)
                .Map(skcp =>
                {
                    skcp.MapKey("SecondaryKitColorId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Continents)
                .WithMany(con => con.Countries)
                .Map(conc =>
                {
                    conc.MapLeftKey("CountryId");
                    conc.MapRightKey("ContinentId");
                    conc.ToTable("CountriesContinents");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
