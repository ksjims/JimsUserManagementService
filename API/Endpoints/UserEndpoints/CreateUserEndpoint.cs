using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate;

namespace UserManagementService.API.Endpoints.UserEndpoints;

public class CreateUserEndpoint : BaseAsyncEndpoint
    .WithRequest<User>
    .WithResponse<User>
{
    private readonly IUserService _userService;

    public CreateUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("users")]
    [SwaggerOperation(Summary = "Creates a new user",
        Description = "Creates a new user",
        OperationId = "User.Create",
        Tags = new[] { "UserEndpoint" })]
    public override async Task<ActionResult<User>> HandleAsync(User user, CancellationToken cancellationToken = new CancellationToken())
    {
        var created = await _userService.CreateAsync(user);

        if (!created)
        {
            return BadRequest();
        }

        return Created($"/users/{user.Id}", user);
    }
}
