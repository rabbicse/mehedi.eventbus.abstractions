namespace Mehedi.EventBus;

/// <summary>
/// Gives an integration event a stable, assembly-independent name, for example
/// <c>[IntegrationEventName("kyc.customer-created.v1")]</c>.
/// </summary>
/// <remarks>
/// Put the same attribute on the producer's and the consumer's contract classes. Renaming or moving either
/// class then no longer breaks the other service. Include a version suffix so a breaking change can ship
/// as a new event name side by side with the old one.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class IntegrationEventNameAttribute : Attribute
{
    /// <summary>Initializes a new instance of the <see cref="IntegrationEventNameAttribute"/> class.</summary>
    /// <param name="name">The event name. Must not be empty.</param>
    public IntegrationEventNameAttribute(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }

    /// <summary>Gets the event name.</summary>
    public string Name { get; }
}
