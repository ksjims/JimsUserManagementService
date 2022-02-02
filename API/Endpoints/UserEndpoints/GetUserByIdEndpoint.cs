using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate;

namespace UserManagementService.API.Endpoints.UserEndpoints;

public class GetUserByIdEndpoint : BaseAsyncEndpoint
    .WithRequest<Guid>
    .WithResponse<User>
{
    private readonly IUserService _userService;

    public GetUserByIdEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("users/{id:guid}")]
    [SwaggerOperation(Summary = "Gets user by id",
        Description = "Gets user by id",
        OperationId = "User.GetById",
        Tags = new[] { "UserEndpoint" })]
    public override async Task<ActionResult<User>> HandleAsync([FromRoute]Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
