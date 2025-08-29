namespace Framework.Mediator.BatchCommands;

public class BatchCommandHandlingParameters(bool continueOnErrors, TimeSpan? iterationDelay = null, TimeSpan? delayOnError = null)
{
    public static readonly BatchCommandHandlingParameters Default = new BatchCommandHandlingParameters(continueOnErrors: false);
    public static readonly BatchCommandHandlingParameters Safe = new BatchCommandHandlingParameters(continueOnErrors: true);

    public bool ContinueOnErrors { get; } = continueOnErrors;
    public TimeSpan? IterationDelay { get; } = iterationDelay;
    public TimeSpan? DelayOnError { get; }
}
