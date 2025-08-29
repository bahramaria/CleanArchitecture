namespace Framework.Mediator.Middlewares;

internal sealed class LastPipe<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> handler) :
    IRequestProcessor<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    public Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return handler.Handle(context.Request, context.CancellationToken);
    }
}