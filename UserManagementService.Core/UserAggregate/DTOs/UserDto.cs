namespace UserManagementService.Core.UserAggregate;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    public UserDto()
    {
    }

    public UserDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

