namespace Mehedi.EventBus;

/// <summary>
/// Handles one type of integration event received from the message broker.
/// </summary>
/// <typeparam name="TEvent">The event contract this handler consumes.</typeparam>
/// <remarks>
/// <para>
/// Brokers deliver at least once, so the same event can arrive more than once (after a restart or a
/// rebalance). Make handlers idempotent: an upsert, or a check of <see cref="IntegrationEvent.Id"/>
/// against already processed ids.
/// </para>
/// <para>
/// Throwing tells the transport that handling failed; it then retries and finally dead-letters the message,
/// depending on the transport's configuration.
/// </para>
/// </remarks>
public interface IIntegrationEventHandler<in TEvent>
    where TEvent : IntegrationEvent
{
    /// <summary>Handles the event.</summary>
    /// <param name="event">The received event.</param>
    /// <param name="cancellationToken">Signals that the host is shutting down.</param>
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
