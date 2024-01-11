using app.Controllers;
using app.Data;
using app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ComicBookDB _dbContext; // Inject YourDbContext

    private AccountController(SignInManager<ApplicationUser> signInManager, ComicBookDB dbContext)
    {
        _signInManager = signInManager;
        _dbContext = dbContext;
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                // Access DbContext here if needed
                // Example: var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}

