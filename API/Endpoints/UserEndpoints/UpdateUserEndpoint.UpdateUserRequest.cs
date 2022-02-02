using Microsoft.AspNetCore.Mvc;
using UserManagementService.Core.UserAggregate;

namespace UserManagementService.API.Endpoints.UserEndpoints;
public record UpdateCustomerRequest
{
    [FromRoute(Name = "id")] public Guid Id { get; init; }
    [FromBody] public User UpdatedUser { get; set; } = default!;
}
