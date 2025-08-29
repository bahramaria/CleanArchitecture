namespace Framework.Mediator.Exceptions;

public sealed class UnexpectedRequestTypeException<TRequest>(object request) : ProgrammerException(ExceptionMessage(typeof(TRequest), request.GetType()))
{
    private static string ExceptionMessage(Type expectedType, Type requestType)
    {
        return $"Unexpected request type encountered: '{requestType}'. Expected type: '{expectedType}'.";
    }
}
