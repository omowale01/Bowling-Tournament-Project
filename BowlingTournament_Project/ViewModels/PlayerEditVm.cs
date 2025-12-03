using System.ComponentModel.DataAnnotations;
namespace BowlingTournament_Project.ViewModels
{
    public class PlayerEditVm
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Player name is required.")]
        public string PlayerName { get; set; } = "";

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = "";

        [Required(ErrorMessage = "Province is required.")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Province must be 2 capital letters (e.g., NB).")]
        public string Province { get; set; } = "";

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = "";

        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone must be in format 506-452-8742.")]
        public string Phone { get; set; } = "";
    }
}
