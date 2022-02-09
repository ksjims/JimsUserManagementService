namespace UserManagementService.Shared;
public interface BaseEntity<TId>
{
    public TId Id { get; set; }
}
