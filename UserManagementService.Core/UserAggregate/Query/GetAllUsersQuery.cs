using MediatR;

namespace UserManagementService.Core.UserAggregate.Query;

public class GetAllUsersQuery : IRequest<List<User>>
{
}