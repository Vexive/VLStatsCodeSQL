namespace VLStatsCodeSQL.Models
{
    public class Player
    {
     
        public int PlayerId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int JerseyNumber { get; set; }

        public decimal? SpikeHeight { get; set; }

        public bool IsRightHanded { get; set; }

        public int? TeamId { get; set; }
    }
}
