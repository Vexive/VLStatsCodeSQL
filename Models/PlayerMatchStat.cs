namespace VLStatsCodeSQL.Models
{
    public class PlayerMatchStat
    {

        public int PlayerId { get; set; }

        public Guid MatchId { get; set; }

        public int PointsScored { get; set; }

        public bool IsStarter { get; set; }
    }
}
