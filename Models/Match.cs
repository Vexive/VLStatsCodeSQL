namespace VLStatsCodeSQL.Models
{
    public class Match
    {
        public Guid MatchId { get; set; }

        public DateTime MatchDate { get; set; }

        public int? TournamentId { get; set; }

        public int? HomeTeamId { get; set; }

        public int? AwayTeamId { get; set; }
    }
}
