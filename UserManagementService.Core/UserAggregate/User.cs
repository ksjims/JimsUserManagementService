namespace UserManagementService.Core.UserAggregate;
using UserManagementService.Shared.Data;
public class User : BaseEntity
{
    public string Name { get; set; } = default!;

    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

