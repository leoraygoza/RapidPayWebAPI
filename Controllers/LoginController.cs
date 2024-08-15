using Microsoft.AspNetCore.Mvc;
using RapidPayWebAPI.Controllers.Requests;
using RapidPayWebAPI.Services;

namespace RapidPayWebAPI.Controllers;

[Route("auth")]
public class LoginController : Controller
{
    private readonly ITokenService _tokenService;

    public LoginController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        // of course, this is a very simple example of authentication
        // in a real-world application, you would use a more secure method
        if (loginRequest.Username != "rapidpay_user" || loginRequest.Password != "rapidpay_password")
        {
            return BadRequest("Login failed");
        }

        var token = _tokenService.GenerateToken(loginRequest.Username);
        return Ok(token);
    }
}