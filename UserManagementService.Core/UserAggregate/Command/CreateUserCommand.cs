using UserManagementService.Core.UserAggregate.DTOs;

namespace UserManagementService.Core.UserAggregate.Command;

public class CreateUserCommand : IRequest<UserDto>
{
    public UserDto UserDto { get; set; }
    public CreateUserCommand(UserDto userDto)
    {
        UserDto = userDto;
    }
}
