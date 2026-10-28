namespace Mehedi.EventBus;

/// <summary>
/// Collects integration events so they are stored together with the business data, in the same
/// transaction, and published afterwards (the transactional outbox pattern).
/// </summary>
/// <remarks>
/// <para>
/// Use it instead of calling <see cref="IEventProducer.PublishAsync"/> right after saving: that is a dual write,
/// and the event is lost when the broker is unavailable or the process stops in between.
/// </para>
/// <para>
/// Call <see cref="Add"/> before the unit of work saves. Nothing is stored if the transaction rolls back.
/// An implementation for EF Core is in <c>Mehedi.Write.RDBMS.Infrastructure.Abstractions</c>.
/// Delivery is at least once, so consumers must be idempotent.
/// </para>
/// </remarks>
public interface IIntegrationEventOutbox
{
    /// <summary>Adds an event to the outbox. It is written by the next save of the current unit of work.</summary>
    void Add(IntegrationEvent integrationEvent);
}
