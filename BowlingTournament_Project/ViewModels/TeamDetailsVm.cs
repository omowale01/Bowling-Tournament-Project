namespace BowlingTournament_Project.ViewModels
{
    public class TeamDetailsVm
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = "";
        public string DivisionName { get; set; } = "";
        public bool RegistrationPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public List<PlayerDetailsVm> Players { get; set; } = new();
    }

    public class PlayerDetailsVm
    {
        public string PlayerName { get; set; } = "";
        public string City { get; set; } = "";
        public string Province { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}
