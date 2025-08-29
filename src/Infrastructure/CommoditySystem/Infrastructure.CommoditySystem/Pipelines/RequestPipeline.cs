using Framework.Mediator;
using Framework.Mediator.Middlewares;
using Infrastructure.RequestAudit;

namespace Infrastructure.CommoditySystem.Pipelines;

internal sealed class RequestPipeline<TRequest, TResponse>(
    IRequestHandler<TRequest, TResponse> handler,
    ExceptionTranslationMiddleware<TRequest, TResponse> exceptionTranslation,
    RequestAuditMiddleware<TRequest, TResponse> audit) : Pipeline<TRequest, TResponse>(handler, exceptionTranslation, audit)
    where TRequest : RequestBase, IRequest<TRequest, TResponse>
{
}