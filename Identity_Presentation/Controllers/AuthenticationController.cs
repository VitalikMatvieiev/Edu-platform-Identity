using Identity_Application.Interfaces;
using Identity_Contracts.Entities.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Presentation.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authresult = _authenticationService.Register(request.Username, request.Email, request.Password);

        var response = new AuthenticationResponse(authresult.Id, authresult.Username, authresult.Email, authresult.Token, authresult.TokenExpiration);

        return Ok(response);
    }

    [HttpPost("Login")]
    public IActionResult Login(LoginRequest request)
    {
        var authresult = _authenticationService.Login(request.Email, request.Password);

        var response = new AuthenticationResponse(authresult.Id, authresult.Username, authresult.Email, authresult.Token, authresult.TokenExpiration);

        return Ok(response);
    }

}