using BowlingTournament_Project.Models;
using System.ComponentModel.DataAnnotations;
namespace BowlingTournament_Project.ViewModels
{
    public class TeamEditVm
    {
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Team name is required.")]
        [StringLength(100, ErrorMessage = "Team name must be 100 characters or fewer.")]
        public string TeamName { get; set; } = "";

        [Required(ErrorMessage = "Division is required.")]
        public int DivisionId { get; set; }

        public bool RegistrationPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Division? Division { get; set; }
        public IEnumerable<Division>? Divisions { get; set; }
        public IEnumerable<Player>? Players { get; set; }
    }
}
