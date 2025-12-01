using System.Numerics;
namespace BowlingTournament_Project.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = "";
        public int DivisionId { get; set; }
        public bool RegistrationPaid { get; set; }
        public DateTime? PaymentDate { get; set; }

        // Navigation properties
        public Division Division { get; set; } = null!;
        public List<Player> Players { get; set; } = new();
    }
}

