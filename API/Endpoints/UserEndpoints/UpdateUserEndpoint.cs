using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagementService.API.Attributes;
using UserManagementService.Core.UserAggregate;
using UserManagementService.Core.UserAggregate.Command;

namespace UserManagementService.API.Endpoints.UserEndpoints;

public class UpdateUserEndpoint : BaseAsyncEndpoint
    .WithRequest<UserDto>
    .WithResponse<User>
{
    private readonly IMediator _mediator;

    public UpdateUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("users/{id:guid}")]
    [SwaggerOperation(Summary = "Updates a user", Description = "Updates a user", OperationId = "User.Update", Tags = new[] { "UserEndpoint" })]
    public override async Task<ActionResult<User>> HandleAsync([FromMultiSource]UserDto userDto, CancellationToken cancellationToken = new CancellationToken())
    {
        var command = new UpdateUserCommand
        {
            UserDto = userDto
        };

        var response = await _mediator.Send(command);

        if (response is null)
        {
            return BadRequest();
        }

        return Ok(response);
    }
}
