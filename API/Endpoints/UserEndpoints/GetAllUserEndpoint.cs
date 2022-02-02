using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate;

namespace UserManagementService.API.Endpoints.UserEndpoints;

public class GetAllUserEndpoint : BaseAsyncEndpoint
    .WithoutRequest
    .WithResponse<List<User>>
{
    private readonly IUserService _userService;

    public GetAllUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("users")]
    [SwaggerOperation(Summary = "Gets all users",
        Description = "Gets all users",
        OperationId = "User.GetAll",
        Tags = new []{ "UserEndpoint" })]
    public override async Task<ActionResult<List<User>>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }
}
