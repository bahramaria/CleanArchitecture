using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Querying.Pipelines;

internal static class QueryPipeline
{
    public sealed class Pipeline<TRequest, TResponse>(IServiceProvider serviceProvider) : KeyedPipeline<TRequest, TResponse>(serviceProvider, Configuration.PipelineName)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
    }

    public sealed class Configuration : IKeyedPipelineConfiguration
    {
        public static string PipelineName { get; } = "QueryingPipeline";

        public static Type[] Middlewares()
        {
            return
            [
                typeof(ExceptionHandlingMiddleware<,>),
                typeof(RequestAuditMiddleware<,>),
                typeof(AuthorizationMiddleware<,>),
                typeof(ValidationMiddleware<,>),
                typeof(FilteringMiddleware<,>),
            ];
        }
    }
}