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

    /// <summary>
    /// Gets the stable name of this event on the wire, for example <c>"kyc.customer-created.v1"</c>.
    /// </summary>
    /// <remarks>
    /// Producers and consumers agree on this name, not on a .NET type, so a consumer can map it to its own
    /// contract class in a different assembly. The name comes from <see cref="IntegrationEventNameAttribute"/>
    /// when present, otherwise from the class name. It is not part of the JSON payload.
    /// </remarks>
    [JsonIgnore]
    public string EventName => IntegrationEventName.Of(GetType());

    /// <summary>
    /// Returns the key that decides the event's partition. Events with the same key are delivered in order.
    /// Defaults to <see cref="Id"/> (no ordering); override it to return the aggregate id and keep an
    /// aggregate's events in order.
    /// </summary>
    /// <remarks>A method rather than a property, so overriding it can never add it to the JSON payload.</remarks>
    public virtual Guid GetPartitionKey() => Id;
}
