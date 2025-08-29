using Framework.Exceptions;

namespace CleanArchitecture.Secrets.Exceptions;

public class EnvironmentVariableNotFoundException(string variable) : ConfigurationException($"Environment Variable '{variable}' is not available!")
{
    public string Variable { get; } = variable;
}
