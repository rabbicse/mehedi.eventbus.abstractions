using System.Text.Json.Serialization;

namespace Mehedi.EventBus;

/// <summary>
/// Represents an integration event.
/// Source: https://github.com/dotnet/eShop/blob/main/src/EventBus/Events/IntegrationEvent.cs
/// </summary>
public record IntegrationEvent
{
    /// <summary>
    /// Gets or sets the unique identifier of the integration event.
    /// </summary>
    [JsonInclude]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the creation date of the integration event in UTC format.
    /// </summary>
    [JsonInclude]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
