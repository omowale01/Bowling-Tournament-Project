using System.ComponentModel.DataAnnotations;

namespace BowlingTournament_Project.ViewModels
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = "";
    }
}