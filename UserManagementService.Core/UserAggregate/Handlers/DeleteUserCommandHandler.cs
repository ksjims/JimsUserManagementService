using MediatR;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate.Command;

namespace UserManagementService.Core.UserAggregate.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserService _userService;
    
    public DeleteUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.DeleteAsync(request.Id);
    }
}