using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using UserManagementService.Shared.Core.Aggregate.Query;
using UserManagementService.Shared.Core.Aggregate.Specification;

namespace UserManagementService.Core.UserAggregate.Specification;

public sealed class UserByIdQuerySpecification<TResponse> : BaseSpecification<User>
{
    private readonly Guid _id;

    public UserByIdQuerySpecification([NotNull] IItemQuery<Guid, TResponse> queryInput)
    {
        ApplyIncludeList(queryInput.Includes);

        _id = queryInput.Id;
    }

    public override Expression<Func<User, bool>> Criteria => p => p.Id == _id;
}

