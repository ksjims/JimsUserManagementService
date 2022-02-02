using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagementService.Core.Interfaces;

namespace UserManagementService.API.Endpoints.UserEndpoints;

public class DeleteUserEndpoint : BaseAsyncEndpoint
    .WithRequest<Guid>
    .WithoutResponse
{
    private readonly IUserService _userService;

    public DeleteUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    [HttpDelete("users/{id:guid}")]
    [SwaggerOperation(Summary = "Deletes a user",
        Description = "Deletes a user",
        OperationId = "User.Delete",
        Tags = new[] { "UserEndpoint" })]
    public override async Task<ActionResult> HandleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
        var deleted = await _userService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}
