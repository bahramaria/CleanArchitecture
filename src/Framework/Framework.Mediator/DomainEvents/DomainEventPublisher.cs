using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator.DomainEvents;

internal sealed class DomainEventPublisher(IServiceProvider serviceProvider) : IDomainEventPublisher
{
    public Task<Result<Empty>> Publish<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : IDomainEvent
    {
        return
            serviceProvider
            .GetRequiredService<DomainEventPublisher<TEvent>>()
            .Publish(@event, cancellationToken);
    }
}