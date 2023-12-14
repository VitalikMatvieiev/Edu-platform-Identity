using Identity_Application.Commands.Identities;
using Identity_Application.Interfaces.Authentication;
using Identity_Application.Queries.Identities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMediator _mediator;
    private readonly IJwtGenerator _jwtGenerator;

    public AuthenticationController(IAuthenticationService authenticationService, IMediator mediator, IJwtGenerator jwtGenerator)
    {
        _authenticationService = authenticationService;
        _mediator = mediator;
        _jwtGenerator = jwtGenerator;
    }

    [HttpPost("Register")]
    public IActionResult Register(string Username, string Email, string Password)
    {
        var authresult = _authenticationService.Register(Username, Email, Password);

        if (!authresult.IsSuccess)
        {
            return BadRequest(authresult.Errors);
        }

        var identity = _mediator.Send(new CreateIdentityCommand(Username, Email, Password)).Result;

        var token = _jwtGenerator.GenerateToken(identity);

        return Ok(token);
    }

    [HttpPost("Login")]
    public IActionResult Login(string Email, string Password)
    {
        var authresult = _authenticationService.Login(Email, Password);

        if (!authresult.IsSuccess)
        {
            return BadRequest(authresult.Errors);
        }

        var identity = _mediator.Send(new GetIdentityByEmailQuery(Email)).Result;

        var token = _jwtGenerator.GenerateToken(identity);

        return Ok(token);
    }
}