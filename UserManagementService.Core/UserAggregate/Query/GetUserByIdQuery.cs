using MediatR;

namespace UserManagementService.Core.UserAggregate.Query;

public class GetUserByIdQuery : IRequest<User?>
{
    public Guid UserId { get; }

    public GetUserByIdQuery(Guid userId)
    {
        UserId = userId;
    }
}