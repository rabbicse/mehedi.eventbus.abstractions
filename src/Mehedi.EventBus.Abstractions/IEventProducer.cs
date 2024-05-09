namespace Mehedi.EventBus;


/// <summary>
/// Represents an interface for an event producer, responsible for publishing integration events.
/// </summary>
public interface IEventProducer
{
    /// <summary>
    /// Publishes the specified integration event asynchronously.
    /// </summary>
    /// <param name="event">The integration event to be published.</param>
    /// <param name="cancellationToken">Optional. The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates whether the event was successfully published.</returns>
    Task<bool> PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken = default);
}


