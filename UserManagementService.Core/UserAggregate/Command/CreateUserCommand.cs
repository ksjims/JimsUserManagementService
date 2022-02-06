using MediatR;

namespace UserManagementService.Core.UserAggregate.Command;

public class CreateUserCommand : IRequest<UserDto>
{
    public UserDto UserDto { get; set; }
}
