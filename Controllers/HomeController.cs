using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace integrate_dotnet_core_create_react_app.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      var vm = new CreateReactAppViewModel(HttpContext);

      return View(vm);
    }

    public async Task<ActionResult> Login()
    {
      var userId = Guid.NewGuid().ToString();
      var claims = new List<Claim>
      {
        new(ClaimTypes.Name, userId)
      };

      var claimsIdentity = new ClaimsIdentity(
        claims,
        CookieAuthenticationDefaults.AuthenticationScheme);
      var authProperties = new AuthenticationProperties();

      await HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(claimsIdentity),
        authProperties);

      return RedirectToAction("Index");
    }

    public async Task<ActionResult> Logout()
    {
      await HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);

      return RedirectToAction("Index");
    }
  }
}
