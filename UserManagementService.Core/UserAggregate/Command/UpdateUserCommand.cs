﻿using MediatR;

namespace UserManagementService.Core.UserAggregate.Command;

public class UpdateUserCommand : IRequest<UserDto>
{
    public UserDto UserDto { get; set; }
}