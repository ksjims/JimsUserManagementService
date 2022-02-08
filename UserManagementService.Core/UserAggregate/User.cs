using UserManagementService.Shared;

namespace UserManagementService.Core.UserAggregate;

public class User : BaseEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

