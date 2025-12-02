using BowlingTournament_Project.Models;

namespace BowlingTournament_Project.ViewModels
{
    public class AdminIndexVm
    {
        public IEnumerable<Team> Teams { get; set; } = new List<Team>();
        public IEnumerable<Division> Divisions { get; set; } = new List<Division>();
        public string? SelectedDivision { get; set; }
        public bool? PaidFilter { get; set; }
    }
}
