using UserManagementService.Core.UserAggregate.DTOs;
using UserManagementService.Shared.Core.Aggregate.Query;

namespace UserManagementService.Core.UserAggregate.Query;

public class GetUserByIdQuery : IItemQuery<Guid, UserDto>
{
    public List<string> Includes { get; init; }
    public Guid Id { get; init; }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
}