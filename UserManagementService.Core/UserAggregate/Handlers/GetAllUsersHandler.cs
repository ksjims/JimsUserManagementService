using MediatR;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate.Query;

namespace UserManagementService.Core.UserAggregate.Handlers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<User>>
{
    private readonly IUserService _userService;

    public GetAllUsersHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetAllAsync();
    }
}
