using System.Collections.Concurrent;
using System.Reflection;

namespace Mehedi.EventBus;

/// <summary>Resolves the wire name of an integration event type.</summary>
public static class IntegrationEventName
{
    private static readonly ConcurrentDictionary<Type, string> Cache = new();

    /// <summary>
    /// Returns the name from <see cref="IntegrationEventNameAttribute"/> when the type has one,
    /// otherwise the type's simple name (for example <c>CustomerCreatedEvent</c>).
    /// </summary>
    /// <param name="eventType">An integration event type.</param>
    public static string Of(Type eventType)
    {
        ArgumentNullException.ThrowIfNull(eventType);

        return Cache.GetOrAdd(eventType, static type =>
            type.GetCustomAttribute<IntegrationEventNameAttribute>(inherit: false)?.Name ?? type.Name);
    }

    /// <summary>Returns the wire name of <typeparamref name="TEvent"/>.</summary>
    public static string Of<TEvent>() where TEvent : IntegrationEvent => Of(typeof(TEvent));
}
