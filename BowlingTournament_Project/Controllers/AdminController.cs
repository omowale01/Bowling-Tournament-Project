using BowlingTournament_Project.Models;
using BowlingTournament_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BowlingTournament_Project.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly BowlingDbContext _db;

        public AdminController(BowlingDbContext db)
        {
            _db = db;
        }

        // MVC-11: Filter Teams
        public IActionResult Index(string? division, bool? paid)
        {
            var query = _db.Teams
                .Include(t => t.Division)
                .Include(t => t.Players)
                .AsQueryable();

            if (!string.IsNullOrEmpty(division) && int.TryParse(division, out int divisionId))
            {
                query = query.Where(t => t.DivisionId == divisionId);
            }

            if (paid.HasValue)
            {
                query = query.Where(t => t.RegistrationPaid == paid.Value);
            }

            var teams = query.OrderBy(t => t.Division.DivisionName)
                           .ThenBy(t => t.TeamName)
                           .ToList();

            var vm = new AdminIndexVm
            {
                Teams = teams,
                Divisions = _db.Divisions.OrderBy(d => d.DivisionName).ToList(),
                SelectedDivision = division,
                PaidFilter = paid
            };

            return View(vm);
        }

        // MVC-06: Create Team (GET)
        public IActionResult CreateTeam()
        {
            var vm = new TeamEditVm
            {
                Divisions = _db.Divisions.OrderBy(d => d.DivisionName).ToList()
            };
            return View(vm);
        }

        // MVC-06: Create Team (POST)
        [HttpPost]
        public IActionResult CreateTeam(TeamEditVm vm)
        {
            if (_db.Teams.Any(t => t.TeamName == vm.TeamName))
            {
                ModelState.AddModelError("TeamName", "Team name already exists.");
            }

            if (!ModelState.IsValid)
            {
                vm.Divisions = _db.Divisions.OrderBy(d => d.DivisionName).ToList();
                return View(vm);
            }

            var team = new Team
            {
                TeamName = vm.TeamName,
                DivisionId = vm.DivisionId,
                RegistrationPaid = vm.RegistrationPaid,
                PaymentDate = vm.RegistrationPaid ? DateTime.Now : null
            };

            _db.Teams.Add(team);
            _db.SaveChanges();

            TempData["Success"] = $"Team '{vm.TeamName}' created successfully!";
            return RedirectToAction("Index");
        }

        // MVC-07: Edit Team (GET)
        public IActionResult EditTeam(int id)
        {
            var team = _db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            var vm = new TeamEditVm
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                DivisionId = team.DivisionId,
                RegistrationPaid = team.RegistrationPaid,
                PaymentDate = team.PaymentDate,
                Divisions = _db.Divisions.OrderBy(d => d.DivisionName).ToList()
            };

            return View(vm);
        }

        // MVC-07: Edit Team (POST)
        [HttpPost]
        public IActionResult EditTeam(TeamEditVm vm)
        {
            if (_db.Teams.Any(t => t.TeamName == vm.TeamName && t.TeamId != vm.TeamId))
            {
                ModelState.AddModelError("TeamName", "Team name already exists.");
            }

            if (!ModelState.IsValid)
            {
                vm.Divisions = _db.Divisions.OrderBy(d => d.DivisionName).ToList();
                return View(vm);
            }

            var team = _db.Teams.Find(vm.TeamId);
            if (team == null) return NotFound();

            team.TeamName = vm.TeamName;
            team.DivisionId = vm.DivisionId;
            team.RegistrationPaid = vm.RegistrationPaid;
            team.PaymentDate = vm.RegistrationPaid ? (vm.PaymentDate ?? DateTime.Now) : null;

            _db.SaveChanges();

            TempData["Success"] = $"Team '{vm.TeamName}' updated successfully!";
            return RedirectToAction("Index");
        }

        // MVC-08: Delete Team (GET)
        public IActionResult DeleteTeam(int id)
        {
            var team = _db.Teams
                .Include(t => t.Division)
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // MVC-08: Delete Team (POST)
        [HttpPost]
        public IActionResult DeleteTeamConfirmed(int id)
        {
            var team = _db.Teams
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            _db.Players.RemoveRange(team.Players);
            _db.Teams.Remove(team);
            _db.SaveChanges();

            TempData["Success"] = $"Team '{team.TeamName}' deleted successfully!";
            return RedirectToAction("Index");
        }

        // MVC-09: Manage Players
        public IActionResult ManagePlayers(int teamId)
        {
            var team = _db.Teams
                .Include(t => t.Players)
                .Include(t => t.Division)
                .FirstOrDefault(t => t.TeamId == teamId);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // MVC-09: Add Player (GET)
        public IActionResult AddPlayer(int teamId)
        {
            var team = _db.Teams.Find(teamId);
            if (team == null)
            {
                return NotFound();
            }

            var vm = new PlayerEditVm { TeamId = teamId };
            return View(vm);
        }

        // MVC-09: Add Player (POST)
        [HttpPost]
        public IActionResult AddPlayer(PlayerEditVm vm)
        {
            var team = _db.Teams
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == vm.TeamId);

            if (team == null)
            {
                return NotFound();
            }

            if (team.Players.Count >= 4)
            {
                ModelState.AddModelError("", "Team already has 4 players. Cannot add more.");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var player = new Player
            {
                TeamId = vm.TeamId,
                PlayerName = vm.PlayerName,
                City = vm.City,
                Province = vm.Province,
                Email = vm.Email,
                Phone = vm.Phone
            };

            _db.Players.Add(player);
            _db.SaveChanges();

            TempData["Success"] = $"Player '{vm.PlayerName}' added successfully!";
            return RedirectToAction("ManagePlayers", new { teamId = vm.TeamId });
        }

        // MVC-09: Remove Player
        [HttpPost]
        public IActionResult RemovePlayer(int id)
        {
            var player = _db.Players
                .Include(p => p.Team)
                .FirstOrDefault(p => p.PlayerId == id);

            if (player == null)
            {
                return NotFound();
            }

            //Prevent going below 4 players
            if (player.Team.Players.Count <= 4)
            {
                TempData["Error"] = "Teams must have exactly 4 players. Cannot remove player.";
                return RedirectToAction("ManagePlayers", new { teamId = player.TeamId });
            }

            var teamId = player.TeamId;
            _db.Players.Remove(player);
            _db.SaveChanges();

            TempData["Success"] = $"Player '{player.PlayerName}' removed successfully!";
            return RedirectToAction("ManagePlayers", new { teamId });
        }

        // MVC-10: Mark Team as Paid
        [HttpPost]
        public IActionResult MarkPaid(int id)
        {
            var team = _db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            team.RegistrationPaid = true;
            team.PaymentDate = DateTime.Now;
            _db.SaveChanges();

            TempData["Success"] = $"Team '{team.TeamName}' marked as paid!";
            return RedirectToAction("Index");
        }

        // MVC-12: View Registration Summary
        public IActionResult Summary()
        {
            var totalTeams = _db.Teams.Count();
            var paidTeams = _db.Teams.Count(t => t.RegistrationPaid);
            var totalFees = paidTeams * 200.00m;

            var vm = new AdminSummaryVm
            {
                TotalTeams = totalTeams,
                PaidTeams = paidTeams,
                TotalFees = totalFees
            };

            return View(vm);
        }

        // MVC-13: Division Summary Report (Bonus)
        public IActionResult DivisionSummary()
        {
            var summary = _db.Divisions
                .Include(d => d.Teams)
                .Select(d => new
                {
                    DivisionName = d.DivisionName,
                    TotalTeams = d.Teams.Count(),
                    PaidTeams = d.Teams.Count(t => t.RegistrationPaid),
                    TotalFees = d.Teams.Count(t => t.RegistrationPaid) * 200.00m
                })
                .OrderBy(s => s.DivisionName)
                .ToList();

            return View(summary);
        }
    }
}
