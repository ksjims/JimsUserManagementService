using UserManagementService.Shared.Core.Aggregate;

namespace UserManagementService.Core.UserAggregate;

public class User : BaseEntity
{
    public string Name { get; set; } = default!;

    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

