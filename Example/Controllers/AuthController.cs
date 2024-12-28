using Example.ApplicationLayer.Auths;
using Example.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Auth(LoginRequest model) => Ok(await _authService.AuthenticateAsync(model));
}