using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagementService.Core.UserAggregate.Command;

namespace UserManagementService.API.Endpoints.UserEndpoints;

public class DeleteUserEndpoint : BaseAsyncEndpoint
    .WithRequest<Guid>
    .WithoutResponse
{
    private readonly IMediator _mediator;

    public DeleteUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("users/{id:guid}")]
    [SwaggerOperation(Summary = "Deletes a user", Description = "Deletes a user", OperationId = "User.Delete", Tags = new[] { "UserEndpoint" })]
    public override async Task<ActionResult> HandleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
        var command = new DeleteUserCommand
        {
            Id = id
        };

        var response = await _mediator.Send(command);

        if (!response)
        {
            return NotFound();
        }

        return Ok();
    }
}
