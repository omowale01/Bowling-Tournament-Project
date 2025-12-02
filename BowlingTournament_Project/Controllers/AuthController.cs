using BowlingTournament_Project.Models;
using BowlingTournament_Project.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BowlingTournament_Project.Controllers
{
    public class AuthController : Controller
    {
        private readonly BowlingDbContext _db;

        public AuthController(BowlingDbContext db)
        {
            _db = db;
        }

        // MVC-04: Login Admin (GET)
        public IActionResult Login()
        {
            return View(new LoginVm());
        }

        // MVC-04: Login Admin (POST)
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await (from u in _db.BowlingUsers
                              where u.UserName == vm.UserName
                              select u).FirstOrDefaultAsync();
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(vm);
            }

            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword(null, user.PasswordHash, vm.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(vm);
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };
            if (user.IsAdmin)
            {
                claims.Add(new Claim("IsAdmin", "true"));
            }

            var identity = new ClaimsIdentity(claims, "app-cookie");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("app-cookie", principal);
            return RedirectToAction("Index", "Admin");
        }

        // MVC-05: Logout
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("app-cookie");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Denied() => View();
    }
}
