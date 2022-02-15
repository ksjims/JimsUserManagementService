using UserManagementService.Shared.Core.Aggregate.DTOs;
using UserManagementService.Shared.Core.Aggregate.Query;
using UserManagementService.Shared.Core.Aggregate.Specification;

namespace UserManagementService.Core.UserAggregate.Specification;

public sealed class UserListQuerySpecification<TResponse> : BaseSpecification<User>
{
    public UserListQuerySpecification(IListQuery<ListResultModel<TResponse>> listQueryInput)
    {
        ApplyIncludeList(listQueryInput.Includes);

        ApplyFilterList(listQueryInput.Filters);

        ApplySortingList(listQueryInput.Sorts);

        ApplyPaging(listQueryInput.Page, listQueryInput.PageSize);
    }
}