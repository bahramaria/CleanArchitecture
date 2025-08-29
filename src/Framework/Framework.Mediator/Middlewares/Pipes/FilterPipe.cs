namespace Framework.Mediator.Middlewares.Pipes;

internal class FilterPipe<TRequest, TResponse>(IMiddleware<TRequest, TResponse> filter, IRequestProcessor<TRequest, TResponse> pipe) : IRequestProcessor<TRequest, TResponse>
{
    public Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return filter.Handle(context, pipe);
    }
}
