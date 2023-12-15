using Identity_Application.Commands.Roles;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Application.Queries.Roles;
using Identity_Domain.Entities.Enums;
using Identity_Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : Controller
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HasClaim(ControllersClaims.ReadRoles)]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var result = await _mediator
            .Send(new GetRoleByIdQuery(id));

        return Ok(result);
    }

    [HasClaim(ControllersClaims.ReadRoles)]
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await _mediator
            .Send(new GetAllRolesQuery());

        return Ok(result);
    }

    [HasClaim(ControllersClaims.WriteRole)]
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleVM roleVM)
    {
        var result = await _mediator
            .Send(new CreateRoleCommand(roleVM));

        return Ok(result);
    }

    [HasClaim(ControllersClaims.ChangeRole)]
    [HttpPut]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleVM roleVM)
    {
        await _mediator
            .Send(new UpdateRoleCommand(id, roleVM));

        return Ok();
    }

    [HasClaim(ControllersClaims.DeleteRole)]
    [HttpDelete]
    public async Task<IActionResult> DeleteRole(int id)
    {
        await _mediator
            .Send(new DeleteRoleCommand(id));

        return Ok();
    }
}