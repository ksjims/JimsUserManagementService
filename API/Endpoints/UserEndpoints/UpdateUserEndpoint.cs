using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagementService.API.Attributes;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate;

namespace UserManagementService.API.Endpoints.UserEndpoints;

public class UpdateUserEndpoint : BaseAsyncEndpoint
    .WithRequest<UpdateCustomerRequest>
    .WithResponse<User>
{
    private readonly IUserService _userService;

    public UpdateUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPut("users/{id:guid}")]
    [SwaggerOperation(Summary = "Updates a user",
        Description = "Updates a user",
        OperationId = "User.Update",
        Tags = new[] { "UserEndpoint" })]
    public override async Task<ActionResult<User>> HandleAsync(
        [FromMultiSource]UpdateCustomerRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userService.GetByIdAsync(request.Id);

        if (user is null)
        {
            return NotFound();
        }

        user.Name = request.UpdatedUser.Name;

        await _userService.UpdateAsync(user);

        return Ok(user);
    }
}
