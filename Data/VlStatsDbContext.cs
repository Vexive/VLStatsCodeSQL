using Microsoft.EntityFrameworkCore;
using VLStatsCodeSQL.Models;

namespace VLStatsCodeSQL.Data
{
    public class VlStatsDbContext : DbContext
    {
        private readonly string _connectionString;

        public VlStatsDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Team> Teams { get; set; } = null!;

        public DbSet<Player> Players { get; set; } = null!;

        public DbSet<Tournament> Tournaments { get; set; } = null!;

        public DbSet<Match> Matches { get; set; } = null!;

        public DbSet<PlayerMatchStat> PlayerMatchStats { get; set; } = null!;

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Teams");
                entity.HasKey(e => e.TeamId);
                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Players");
                entity.HasKey(e => e.PlayerId);
                entity.Property(e => e.PlayerId)
                    .HasColumnName("PlayerID");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.SpikeHeight)
                    .HasColumnType("decimal(4,2)");
                entity.Property(e => e.TeamId)
                    .HasColumnName("TeamID");
                entity.HasIndex(e => new { e.TeamId, e.JerseyNumber })
                    .IsUnique();
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.ToTable("Tournaments");
                entity.HasKey(e => e.TournamentId);
                entity.Property(e => e.TournamentId)
                    .HasColumnName("TournamentID");
                entity.Property(e => e.TournamentName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("Matches");
                entity.HasKey(e => e.MatchId);
                entity.Property(e => e.MatchId)
                    .HasColumnName("MatchID")
                    .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.TournamentId)
                    .HasColumnName("TournamentID");
                entity.Property(e => e.HomeTeamId)
                    .HasColumnName("HomeTeamID");
                entity.Property(e => e.AwayTeamId)
                    .HasColumnName("AwayTeamID");
            });

            modelBuilder.Entity<PlayerMatchStat>(entity =>
            {
                entity.ToTable("PlayerMatchStats");
                entity.HasKey(e => new { e.PlayerId, e.MatchId });
                entity.Property(e => e.PlayerId)
                    .HasColumnName("PlayerID");
                entity.Property(e => e.MatchId)
                    .HasColumnName("MatchID");
                entity.Property(e => e.PointsScored)
                    .HasDefaultValue(0);
                entity.Property(e => e.IsStarter)
                    .HasDefaultValue(false);
            });
        }
    }
}
