using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagementService.Core.UserAggregate;
using UserManagementService.Core.UserAggregate.Query;

namespace UserManagementService.API.Endpoints.UserEndpoints;

public class GetUserByIdEndpoint : BaseAsyncEndpoint
    .WithRequest<Guid>
    .WithResponse<User>
{
    private readonly IMediator _mediator;

    public GetUserByIdEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("users/{id:guid}")]
    [SwaggerOperation(Summary = "Gets user by id", Description = "Gets user by id", OperationId = "User.GetById", Tags = new[] { "UserEndpoint" })]
    public override async Task<ActionResult<User>> HandleAsync([FromRoute]Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
        var query = new GetUserByIdQuery(id);
        var user = await _mediator.Send(query);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
