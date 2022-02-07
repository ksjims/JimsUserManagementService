using MediatR;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.Services;
using UserManagementService.Core.UserAggregate;
using UserManagementService.Core.UserAggregate.Command;
using UserManagementService.Core.UserAggregate.Query;

namespace UserManagementService.API.Endpoints;

public static class UserEndpoint
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/users", async (IMediator mediator) => await GetAllCustomersAsync(mediator))
            .Produces(200, typeof(List<UserDto>))
            .Produces(404)
            .WithName("Gets all users")
            .WithTags("UserEndpoint");

        //app.MapGet("users", async (IMediator mediator) => await mediator.Send(new GetAllUsersQuery())).WithTags("UserEndpoint");
        app.MapGet("users/{id}", async (IMediator mediator, Guid id) => await mediator.Send(new GetUserByIdQuery(id))).WithTags("UserEndpoint");
        app.MapPost("users", async (IMediator mediator, UserDto userDto) => await mediator.Send(new CreateUserCommand(userDto))).WithTags("UserEndpoint");
        app.MapPut("users", async (IMediator mediator, UserDto userDto) => await mediator.Send(new UpdateUserCommand(userDto))).WithTags("UserEndpoint");
        app.MapDelete("/users/{id}", async (IMediator mediator, Guid id) => await mediator.Send(new DeleteUserCommand(id))).WithTags("UserEndpoint");
    }

    public static void AddUserServices(this IServiceCollection services)
    {
        services.AddSingleton<IUserService, UserService>();
        services.AddMediatR(typeof(User));
    }

    internal static async Task<IResult> GetAllCustomersAsync(IMediator mediator)
    {
        var query = new GetAllUsersQuery();
        var users = await mediator.Send(query);

        if (users is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(users.Select(u => new UserDto(u.Id, u.Name)));
    }
}
