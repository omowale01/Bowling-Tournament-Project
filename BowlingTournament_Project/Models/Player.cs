namespace BowlingTournament_Project.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public string PlayerName { get; set; } = "";
        public string City { get; set; } = "";
        public string Province { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

        // Navigation property
        public Team Team { get; set; } = null!;
    }
}
