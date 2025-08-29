using CleanArchitecture.Ordering.Persistence;
using Framework.Application;
using Framework.Application.Extensions;
using Framework.Mediator.IntegrationEvents;
using Framework.Mediator.Middlewares;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal sealed class TransactionScopeMiddleware<TRequest, TResponse>(
    OrderingDbContext db,
    IIntegrationEventOutbox eventOutbox,
    IIntegrationEventBus eventBus) :
    IMiddleware<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        var cancellationToken = context.CancellationToken;

        using var transaction = await eventOutbox.BeginTransaction(db, cancellationToken);

        var result = await next.Handle(context);

        if (result.IsFailure)
        {
            return result;
        }

        await db.SaveChangesAsync(cancellationToken);
        await eventOutbox.PublishEvents(eventBus, cancellationToken);

        transaction.Commit();

        return result;
    }
}