using BowlingTournament_Project.Models;
using System.ComponentModel.DataAnnotations;
namespace BowlingTournament_Project.ViewModels
{
    public class TeamRegisterVm
    {
        [Required(ErrorMessage = "Team name is required.")]
        [StringLength(100, ErrorMessage = "Team name must be 100 characters or fewer.")]
        public string TeamName { get; set; } = "";

        [Required(ErrorMessage = "Division is required.")]
        public int DivisionId { get; set; }

        public IEnumerable<Division>? Divisions { get; set; }

        // Player 1
        [Required(ErrorMessage = "Player 1 name is required.")]
        public string Player1Name { get; set; } = "";

        [Required(ErrorMessage = "Player 1 city is required.")]
        public string Player1City { get; set; } = "";

        [Required(ErrorMessage = "Province is required.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Province must be 2 letters.")]
        [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "Province must be 2 capital letters (e.g., NB).")]
        public string Player1Province { get; set; } = "";

        [Required(ErrorMessage = "Player 1 email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Player1Email { get; set; } = "";

        [RegularExpression(@"^(\d{3}-\d{3}-\d{4}|)$", ErrorMessage = "Phone must be in format 506-452-8742")]
        public string Player1Phone { get; set; } = "";

        // Player 2
        [Required(ErrorMessage = "Player 2 name is required.")]
        public string Player2Name { get; set; } = "";

        [Required(ErrorMessage = "Player 2 city is required.")]
        public string Player2City { get; set; } = "";

        [Required(ErrorMessage = "Province is required.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Province must be 2 letters.")]
        [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "Province must be 2 capital letters (e.g., NB).")]
        public string Player2Province { get; set; } = "";

        [Required(ErrorMessage = "Player 2 email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Player2Email { get; set; } = "";

        [RegularExpression(@"^(\d{3}-\d{3}-\d{4}|)$", ErrorMessage = "Phone must be in format 506-452-8742")]
        public string Player2Phone { get; set; } = "";

        // Player 3
        [Required(ErrorMessage = "Player 3 name is required.")]
        public string Player3Name { get; set; } = "";

        [Required(ErrorMessage = "Player 3 city is required.")]
        public string Player3City { get; set; } = "";

        [Required(ErrorMessage = "Province is required.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Province must be 2 letters.")]
        [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "Province must be 2 capital letters (e.g., NB).")]
        public string Player3Province { get; set; } = "";

        [Required(ErrorMessage = "Player 3 email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Player3Email { get; set; } = "";

        [RegularExpression(@"^(\d{3}-\d{3}-\d{4}|)$", ErrorMessage = "Phone must be in format 506-452-8742")]
        public string Player3Phone { get; set; } = "";

        // Player 4
        [Required(ErrorMessage = "Player 4 name is required.")]
        public string Player4Name { get; set; } = "";

        [Required(ErrorMessage = "Player 4 city is required.")]
        public string Player4City { get; set; } = "";

        [Required(ErrorMessage = "Province is required.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Province must be 2 letters.")]
        [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "Province must be 2 capital letters (e.g., NB).")]
        public string Player4Province { get; set; } = "";

        [Required(ErrorMessage = "Player 4 email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Player4Email { get; set; } = "";

        [RegularExpression(@"^(\d{3}-\d{3}-\d{4}|)$", ErrorMessage = "Phone must be in format 506-452-8742")]
        public string Player4Phone { get; set; } = "";
    }
}
