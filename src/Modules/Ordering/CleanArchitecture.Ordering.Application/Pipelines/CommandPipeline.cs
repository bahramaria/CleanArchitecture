using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal static class CommandPipeline
{
    public sealed class Pipeline<TRequest, TResponse>(IServiceProvider serviceProvider) : KeyedPipeline<TRequest, TResponse>(serviceProvider, Configuration.PipelineName)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
    }

    public sealed class Configuration : IKeyedPipelineConfiguration
    {
        public static string PipelineName { get; } = "OrderingCommandPipeline";

        public static Type[] Middlewares()
        {
            return
            [
                typeof(RequestContextMiddleware<,>),
                typeof(ExceptionHandlingMiddleware<,>),
                typeof(RequestAuditMiddleware<,>),
                typeof(AuthorizationMiddleware<,>),
                typeof(ValidationMiddleware<,>),
                typeof(TransactionScopeMiddleware<,>),
                typeof(OrderRequestMiddleware<,>),
            ];
        }
    }
}