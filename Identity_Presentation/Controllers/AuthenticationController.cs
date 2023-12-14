using Identity_Application.Commands.Authentication;
using Identity_Application.Queries.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    public IActionResult Register(string Username, string Email, string Password)
    {
        var token = _mediator.Send(new RegisterCommand(Username, Email, Password));

        return Ok(token);
    }

    [HttpPost("Login")]
    public IActionResult Login(string Email, string Password)
    {
        var token = _mediator.Send(new LoginQuery(Email, Password));

        return Ok(token);
    }
}