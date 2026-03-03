
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(IPublishEndpoint publisherEndpoint
    , IFeatureManager fetureManager
    , ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handler: {DomainEvent}", domainEvent.GetType().Name);


        if (await fetureManager.IsEnabledAsync("OrderFullfilment"))
        {
            var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
            await publisherEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }     

    }
}
