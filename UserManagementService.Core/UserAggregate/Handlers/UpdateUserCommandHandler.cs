using MediatR;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate.Command;
using UserManagementService.Core.UserAggregate.DTOs;

namespace UserManagementService.Core.UserAggregate.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserService _userService;
    
    public UpdateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updated = await _userService.UpdateAsync(new User(request.UserDto.Id, request.UserDto.Name));

        if (updated)
        {
            var user = await _userService.GetByIdAsync(request.UserDto.Id);
            if (user is not null)
            {
                return new UserDto(user.Id, user.Name);
            }
        }

        return null;
    }
}