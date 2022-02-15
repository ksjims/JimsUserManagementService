using MediatR;
using UserManagementService.Shared.Core.Aggregate.DTOs;

namespace UserManagementService.Shared.Core.Aggregate.Query;

public interface IQuery<T> : IRequest<ResultModel<T>> where T : notnull
{
}
