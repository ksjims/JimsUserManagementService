using MediatR;
using UserManagementService.Shared.Core.Aggregate.DTOs;

namespace UserManagementService.Shared.Core.Aggregate.Command;

public interface ICommand<T> : IRequest<ResultModel<T>> where T : notnull
{
}
