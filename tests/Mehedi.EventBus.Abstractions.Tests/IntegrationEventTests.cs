using System.Text.Json;

namespace Mehedi.EventBus.Abstractions.Tests;

public class IntegrationEventTests
{
    private sealed record PlainEvent : IntegrationEvent;

    [IntegrationEventName("sales.order-placed.v1")]
    private sealed record NamedEvent(Guid OrderId) : IntegrationEvent
    {
        public override Guid PartitionKey => OrderId;
    }

    [Fact]
    public void An_event_without_the_attribute_is_named_after_its_class()
    {
        Assert.Equal("PlainEvent", new PlainEvent().EventName);
        Assert.Equal("PlainEvent", IntegrationEventName.Of<PlainEvent>());
    }

    [Fact]
    public void The_attribute_sets_the_wire_name()
    {
        Assert.Equal("sales.order-placed.v1", new NamedEvent(Guid.NewGuid()).EventName);
        Assert.Equal("sales.order-placed.v1", IntegrationEventName.Of(typeof(NamedEvent)));
    }

    [Fact]
    public void The_partition_key_defaults_to_the_event_id()
    {
        var @event = new PlainEvent();

        Assert.Equal(@event.Id, @event.PartitionKey);
    }

    [Fact]
    public void The_partition_key_can_be_the_aggregate_id()
    {
        var orderId = Guid.NewGuid();

        Assert.Equal(orderId, new NamedEvent(orderId).PartitionKey);
    }

    [Fact]
    public void Name_and_partition_key_are_not_part_of_the_payload()
    {
        var json = JsonSerializer.Serialize(new NamedEvent(Guid.NewGuid()));

        Assert.DoesNotContain(nameof(IntegrationEvent.EventName), json, StringComparison.Ordinal);
        Assert.DoesNotContain(nameof(IntegrationEvent.PartitionKey), json, StringComparison.Ordinal);
        Assert.Contains(nameof(IntegrationEvent.Id), json, StringComparison.Ordinal);
    }

    [Fact]
    public void An_empty_name_is_rejected()
    {
        Assert.Throws<ArgumentException>(() => new IntegrationEventNameAttribute(" "));
    }
}
