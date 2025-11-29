using BeFitt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeFitt.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly UserManager<Uzytkownicy> _userManager;

        public RolesController(UserManager<Uzytkownicy> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");

                return Content($"Gratulacje! Użytkownik {user.UserName} otrzymał rolę Admin.");
            }

            return Content("Błąd: Nie znaleziono użytkownika.");
        }
    }
}