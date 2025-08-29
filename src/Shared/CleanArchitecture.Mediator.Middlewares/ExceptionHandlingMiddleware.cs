using Framework.Exceptions;
using Framework.Exceptions.Extensions;
using Framework.Mediator.Middlewares;
using Framework.Results.Errors;
using Framework.Results.Exceptions;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class ExceptionHandlingMiddleware<TRequest, TResponse>(ILogger<ExceptionHandlingMiddleware<TRequest, TResponse>> logger) :
    IMiddleware<TRequest, TResponse>
{
    private readonly ILogger logger = logger;

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        try
        {
            return await next.Handle(context);
        }
        catch (Exception exp)
        {
            logger.LogError(exp, "{@Request} {@Error}", context.Request, exp);

            var systemException = exp.TranslateToSystemException();
            return Errors(systemException);
        }
    }

    private static Error[] Errors(BaseSystemException exception)
    {
        return exception switch
        {
            DomainErrorsException domainErrorsException => domainErrorsException.Errors,
            _ => [.. exception.Messages.Select(x => new FailureError(x))],
        };
    }
}