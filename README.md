[![Nuget](https://img.shields.io/nuget/v/Mehedi.EventBus.Abstractions)](https://www.nuget.org/packages/Mehedi.EventBus.Abstractions/)
[![Nuget](https://img.shields.io/nuget/dt/Mehedi.EventBus.Abstractions)](https://www.nuget.org/packages/Mehedi.EventBus.Abstractions/)

# Mehedi.EventBus.Abstractions
Some useful base classes and interfaces, mainly used with the Message Brokers. Apache Kafka, RabbitMQ or any other message broker integration will use or implement in generic fashion inside ASP.NET Core applications.

## What's inside

| Type | Purpose |
|---|---|
| `IntegrationEvent` | Base record for events that cross service boundaries (`Id`, `CreationDate`, the wire `EventName` and an overridable `GetPartitionKey()`). |
| `IntegrationEventNameAttribute` | Gives an event a stable, versioned name so producer and consumer don't share a .NET type. |
| `IEventProducer` | Publishes an integration event. |
| `IIntegrationEventHandler<TEvent>` | Handles one event type on the consuming side (resolved from DI by the transport). |
| `IEventConsumer` | Low-level, event-based consumer (kept for compatibility; prefer typed handlers). |

## Usage

Define the contract once per service. The two classes can live in different assemblies, because they only share the name:

```csharp
// Producer (KYC service)
[IntegrationEventName("kyc.customer-created.v1")]
public sealed record CustomerCreatedEvent(Guid CustomerId, string FirstName, string LastName) : IntegrationEvent
{
    // Keep all events of one customer in order (same Kafka partition).
    public override Guid GetPartitionKey() => CustomerId;
}

await producer.PublishAsync(new CustomerCreatedEvent(id, "Ada", "Lovelace"), cancellationToken);
```

```csharp
// Consumer (Ordering service) - its own copy of the contract
[IntegrationEventName("kyc.customer-created.v1")]
public sealed record CustomerCreatedEvent(Guid CustomerId, string FirstName, string LastName) : IntegrationEvent;

public sealed class CustomerCreatedHandler(IKnownCustomers customers) : IIntegrationEventHandler<CustomerCreatedEvent>
{
    // Delivery is at least once: make the handler idempotent (an upsert here).
    public Task HandleAsync(CustomerCreatedEvent @event, CancellationToken cancellationToken) =>
        customers.UpsertAsync(@event.CustomerId, $"{@event.FirstName} {@event.LastName}", cancellationToken);
}
```

Register handlers with your transport, for example `Mehedi.EventBus.Kafka`'s `AddKafkaConsumer(...).AddHandler<CustomerCreatedEvent, CustomerCreatedHandler>()`.

### Naming events

- Use `<service>.<event>.v<version>`, for example `kyc.customer-created.v1`.
- A breaking change to the payload is a new name (`.v2`); publish both while consumers migrate.
- Without the attribute the name is the class name, which breaks silently when someone renames the class.

## Technologies
- .NET Core 8
- C#
- xUnit

## Prerequisites
- net8.0

## References
- [eShop](https://github.com/dotnet/eShop/tree/main/src/EventBus)
- [SuperSafeBank](https://github.com/mizrael/SuperSafeBank)
