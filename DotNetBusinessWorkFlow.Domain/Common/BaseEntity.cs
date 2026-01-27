using System.Text.Json.Serialization;

namespace DotNetBusinessWorkFlow.Domain.Common;

public abstract class BaseEntity
{
    [JsonPropertyOrder(1)]
    public Guid Id { get; set; }
}
