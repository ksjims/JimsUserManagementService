namespace UserManagementService.Shared.Core.Aggregate.Command;

public interface ICreateCommand<TRequest, TResponse> : ICommand<TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    public TRequest Model { get; init; }
}
