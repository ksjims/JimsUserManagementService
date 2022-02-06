using MediatR;

namespace UserManagementService.Core.UserAggregate.Command;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
