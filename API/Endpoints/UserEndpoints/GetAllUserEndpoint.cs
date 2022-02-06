using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagementService.Core.UserAggregate;
using UserManagementService.Core.UserAggregate.Query;

namespace UserManagementService.API.Endpoints.UserEndpoints;

public class GetAllUserEndpoint : BaseAsyncEndpoint
    .WithoutRequest
    .WithResponse<List<User>>
{
    private readonly IMediator _mediator;

    public GetAllUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("users")]
    [SwaggerOperation(Summary = "Gets all users", Description = "Gets all users", OperationId = "User.GetAll", Tags = new []{ "UserEndpoint" })]
    public override async Task<ActionResult<List<User>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query);
        return Ok(users);
    }
}
