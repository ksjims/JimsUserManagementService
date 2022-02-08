using MediatR;
using UserManagementService.Core.UserAggregate.DTOs;

namespace UserManagementService.Core.UserAggregate.Command;

public class UpdateUserCommand : IRequest<UserDto>
{
    public UserDto UserDto { get; set; }

    public UpdateUserCommand(UserDto userDto)
    {
        UserDto = userDto;
    }
}
