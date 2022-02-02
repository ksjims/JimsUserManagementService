using UserManagementService.Shared;

namespace UserManagementService.Core.UserAggregate;

public class User : BaseEntity
{
    public string Name { get; set; } = default!;
}

