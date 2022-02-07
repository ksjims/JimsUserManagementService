using MediatR;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.Services;
using UserManagementService.Core.UserAggregate;
using UserManagementService.Core.UserAggregate.Command;
using UserManagementService.Core.UserAggregate.Query;

namespace UserManagementService.API.Endpoints;

public class UserEndpointDefinition : IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<IUserService, UserService>();
        services.AddMediatR(typeof(User));
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("users", async (IMediator mediator) => await GetAllUsersAsync(mediator))
            .Produces(200, typeof(List<UserDto>))
            .Produces(204)
            .WithName("Gets all users")
            .WithTags("UserEndpoint");

        //app.MapGet("users", async (IMediator mediator) => await mediator.Send(new GetAllUsersQuery())).WithTags("UserEndpoint");
        app.MapGet("users/{id}", async (IMediator mediator, Guid id) => await mediator.Send(new GetUserByIdQuery(id))).WithTags("UserEndpoint");
        app.MapPost("users", async (IMediator mediator, UserDto userDto) => await mediator.Send(new CreateUserCommand(userDto))).WithTags("UserEndpoint");
        app.MapPut("users", async (IMediator mediator, UserDto userDto) => await mediator.Send(new UpdateUserCommand(userDto))).WithTags("UserEndpoint");
        app.MapDelete("users/{id}", async (IMediator mediator, Guid id) => await mediator.Send(new DeleteUserCommand(id))).WithTags("UserEndpoint");
    }

    public async Task<IResult> GetAllUsersAsync(IMediator mediator)
    {
        var query = new GetAllUsersQuery();
        var users = await mediator.Send(query);

        if (users is null || !users.Any())
        {
            return Results.NoContent();
        }

        return Results.Ok(users.Select(u => new UserDto(u.Id, u.Name)).ToList());
    }
}
