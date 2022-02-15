namespace UserManagementService.Shared.Data;

public abstract class BaseEntity
{
    public Guid Id { get; protected init; } = Guid.NewGuid();
    public DateTime CreatedDate { get; protected init; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; protected set; }
}
