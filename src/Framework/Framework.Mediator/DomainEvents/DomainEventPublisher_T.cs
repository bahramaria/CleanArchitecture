namespace Framework.Mediator.DomainEvents;

internal sealed class DomainEventPublisher<TEvent>(IEnumerable<IDomainEventHandler<TEvent>> handlers)
    where TEvent : IDomainEvent
{
    public async Task<Result<Empty>> Publish(TEvent @event, CancellationToken cancellationToken)
    {
        foreach (var handler in handlers)
        {
            var result = await handler.Handle(@event, cancellationToken);

            if (result.IsFailure)
            {
                return result;
            }
        }

        return Empty.Value;
    }
}