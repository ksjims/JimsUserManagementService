using MediatR;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate.Command;

namespace UserManagementService.Core.UserAggregate.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserService _userService;
    
    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var created = await _userService.CreateAsync(new User(request.UserDto.Id, request.UserDto.Name));

        if (created) 
        {
            var user = await _userService.GetByIdAsync(request.UserDto.Id);
            return new UserDto(user.Id, user.Name);
        }

        return null;
    }
}