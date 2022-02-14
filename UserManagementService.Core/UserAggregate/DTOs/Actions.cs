using System.Text.Json.Serialization;

namespace UserManagementService.Core.UserAggregate.DTOs;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Actions
{
    Create,
    Update,
    Delete
}

