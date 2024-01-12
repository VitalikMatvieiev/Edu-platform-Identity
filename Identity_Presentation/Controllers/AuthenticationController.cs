using Identity_Application.Commands.Authentication;
using Identity_Application.Models.AuthorizationModels;
using Identity_Application.Queries.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterVM vm)
    {
        var token = await _mediator.Send(new RegisterCommand(vm));

        return Ok(token);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginVM vm)
    {
        var token = await _mediator.Send(new LoginQuery(vm));

        return Ok(token);
    }
}