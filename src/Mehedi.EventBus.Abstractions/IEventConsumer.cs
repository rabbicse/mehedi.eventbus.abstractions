namespace Mehedi.EventBus;


/// <summary>
/// Represents an interface for an event consumer, responsible for consuming integration events.
/// </summary>
public interface IEventConsumer
{
    /// <summary>
    /// Starts consuming integration events asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Optional. The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task StartConsumeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Subscribe for a topic
    /// </summary>
    /// <param name="topicName"></param>
    /// <returns></returns>
    Task<bool> Subscribe(string topicName);

    /// <summary>
    /// Event raised when an integration event is received.
    /// </summary>
    event EventReceivedHandler EventReceived;

    /// <summary>
    /// Event raised when an exception occurs during event consumption.
    /// </summary>
    event ExceptionThrownHandler ExceptionThrown;
}

/// <summary>
/// Represents a delegate for handling integration event received events.
/// </summary>
/// <param name="sender">The event sender.</param>
/// <param name="event">The integration event received.</param>
/// <returns>A task representing the asynchronous operation.</returns>
public delegate Task EventReceivedHandler(object sender, IntegrationEvent @event);

/// <summary>
/// Represents a delegate for handling exceptions thrown during event consumption.
/// </summary>
/// <param name="sender">The event sender.</param>
/// <param name="e">The exception thrown.</param>
public delegate void ExceptionThrownHandler(object sender, Exception e);