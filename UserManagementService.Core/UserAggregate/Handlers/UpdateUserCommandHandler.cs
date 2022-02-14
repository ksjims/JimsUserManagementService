using SQS.Publisher;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate.Command;
using UserManagementService.Core.UserAggregate.DTOs;

namespace UserManagementService.Core.UserAggregate.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserService _userService;
    private readonly ISqsPublisher _sqsPublisher;

    public UpdateUserCommandHandler(IUserService userService, ISqsPublisher sqsPublisher)
    {
        _userService = userService;
        _sqsPublisher = sqsPublisher;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updated = await _userService.UpdateAsync(new User(request.UserDto.Id, request.UserDto.Name));

        if (updated)
        {
            var user = await _userService.GetByIdAsync(request.UserDto.Id);
            if (user is not null)
            {
                await _sqsPublisher.SendMessage(new UserActionDto(user.Id, Actions.Update));
                return new UserDto(user.Id, user.Name);
            }
        }

        return null;
    }
}