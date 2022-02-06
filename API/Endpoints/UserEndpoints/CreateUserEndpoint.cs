using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagementService.Core.UserAggregate;
using UserManagementService.Core.UserAggregate.Command;

namespace UserManagementService.API.Endpoints.UserEndpoints;

public class CreateUserEndpoint : BaseAsyncEndpoint
    .WithRequest<UserDto>
    .WithResponse<User>
{
    private readonly IMediator _mediator;

    public CreateUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("users")]
    [SwaggerOperation(Summary = "Creates a new user", Description = "Creates a new user", OperationId = "User.Create", Tags = new[] { "UserEndpoint" })]
    public override async Task<ActionResult<User>> HandleAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand
        {
            UserDto = userDto
        };

        var response = await _mediator.Send(command);

        if (response is null)
        {
            return BadRequest();
        }

        return Created($"/users/{response.Id}", response);
    }
}
