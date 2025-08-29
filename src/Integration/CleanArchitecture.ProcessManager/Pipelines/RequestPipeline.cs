using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.ProcessManager.Pipelines;

internal static class RequestPipeline
{
    public sealed class Pipeline<TRequest, TResponse>(IServiceProvider serviceProvider) : KeyedPipeline<TRequest, TResponse>(serviceProvider, Configuration.PipelineName)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
    }

    public sealed class Configuration : IKeyedPipelineConfiguration
    {
        public static string PipelineName { get; } = "ProcessManagerPipeline";

        public static Type[] Middlewares()
        {
            return
            [
                typeof(ExceptionHandlingMiddleware<,>),
                typeof(ValidationMiddleware<,>),
            ];
        }
    }
}