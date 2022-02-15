using MediatR;
using UserManagementService.Core.UserAggregate.DTOs;
using UserManagementService.Core.UserAggregate.Query;
using UserManagementService.Core.UserAggregate.Specification;
using UserManagementService.Shared.Core.Aggregate.DTOs;
using UserManagementService.Shared.Core.Interfaces;

namespace UserManagementService.Core.UserAggregate.Handlers;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultModel<UserDto>>
{
    private readonly IRepository<User> _userRepository;
    public GetUserByIdHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultModel<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var spec = new UserByIdQuerySpecification<UserDto>(request);

        var user = await _userRepository.FindOneAsync(spec);

        return ResultModel<UserDto>.Create(new UserDto(user.Id, user.Name));
    }
}
