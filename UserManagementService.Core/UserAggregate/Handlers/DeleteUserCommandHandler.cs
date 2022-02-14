using SQS.Publisher;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate.Command;
using UserManagementService.Core.UserAggregate.DTOs;

namespace UserManagementService.Core.UserAggregate.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserService _userService;
    private readonly ISqsPublisher _sqsPublisher;

    public DeleteUserCommandHandler(IUserService userService, ISqsPublisher sqsPublisher)
    {
        _userService = userService;
        _sqsPublisher = sqsPublisher;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        bool result = await _userService.DeleteAsync(request.Id);

        if (result)
        {
            await _sqsPublisher.SendMessage(new UserActionDto(request.Id, Actions.Delete));
        }

        return result;
    }
}