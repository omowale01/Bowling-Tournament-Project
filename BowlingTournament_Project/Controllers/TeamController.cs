using BowlingTournament_Project.Models;
using BowlingTournament_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BowlingTournament_Project.Controllers
{
    public class TeamController : Controller
    {
        private readonly BowlingDbContext _db;

        public TeamController(BowlingDbContext db)
        {
            _db = db;
        }

        // MVC-02: View Paid Teams
        public IActionResult Paid()
        {
            var paidTeams = _db.Teams
                .Include(t => t.Division)
                .Include(t => t.Players)
                .Where(t => t.RegistrationPaid)
                .OrderBy(t => t.Division.DivisionName)
                .ThenBy(t => t.TeamName)
                .ToList();

            return View(paidTeams);
        }

        // MVC-03: View Team Details
        public IActionResult Details(int id)
        {
            var team = _db.Teams
                .Include(t => t.Division)
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            var vm = new TeamDetailsVm
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                DivisionName = team.Division.DivisionName,
                RegistrationPaid = team.RegistrationPaid,
                PaymentDate = team.PaymentDate,
                Players = team.Players.Select(p => new PlayerDetailsVm
                {
                    PlayerName = p.PlayerName,
                    City = p.City,
                    Province = p.Province,
                    Email = p.Email,
                    Phone = p.Phone
                }).ToList()
            };

            return View(vm);
        }

        // MVC-01: Register Team (GET)
        public IActionResult Register()
        {
            var vm = new TeamRegisterVm
            {
                Divisions = _db.Divisions.OrderBy(d => d.DivisionName).ToList()
            };
            return View(vm);
        }

        // MVC-01: Register Team (POST)
        [HttpPost]
        public IActionResult Register(TeamRegisterVm vm)
        {
            // NEW: Validate all 4 players have data
            var playerFields = new[] {
        vm.Player1Name, vm.Player2Name, vm.Player3Name, vm.Player4Name
            };

            if (playerFields.Any(string.IsNullOrWhiteSpace))
            {
                ModelState.AddModelError("", "All 4 players must have names.");
            }

            // Check if team name already exists
            if (_db.Teams.Any(t => t.TeamName == vm.TeamName))
            {
                ModelState.AddModelError("TeamName", "Team name already exists.");
            }

            if (!ModelState.IsValid)
            {
                vm.Divisions = _db.Divisions.OrderBy(d => d.DivisionName).ToList();
                return View(vm);
            }

            // Create team
            var team = new Team
            {
                TeamName = vm.TeamName,
                DivisionId = vm.DivisionId,
                RegistrationPaid = false,
                PaymentDate = null
            };

            _db.Teams.Add(team);
            _db.SaveChanges();

            // Create players
            var players = new List<Player>
            {
                new Player { TeamId = team.TeamId, PlayerName = vm.Player1Name, City = vm.Player1City, Province = vm.Player1Province, Email = vm.Player1Email, Phone = vm.Player1Phone },
                new Player { TeamId = team.TeamId, PlayerName = vm.Player2Name, City = vm.Player2City, Province = vm.Player2Province, Email = vm.Player2Email, Phone = vm.Player2Phone },
                new Player { TeamId = team.TeamId, PlayerName = vm.Player3Name, City = vm.Player3City, Province = vm.Player3Province, Email = vm.Player3Email, Phone = vm.Player3Phone },
                new Player { TeamId = team.TeamId, PlayerName = vm.Player4Name, City = vm.Player4City, Province = vm.Player4Province, Email = vm.Player4Email, Phone = vm.Player4Phone }
            };

            _db.Players.AddRange(players);
            _db.SaveChanges();

            TempData["Success"] = $"Team '{vm.TeamName}' registered successfully! Payment must be completed to appear in the public list.";
            return RedirectToAction("Index", "Home");
        }
    }
}
