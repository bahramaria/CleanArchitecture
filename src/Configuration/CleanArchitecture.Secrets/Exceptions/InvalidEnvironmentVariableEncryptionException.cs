using Framework.Exceptions;

namespace CleanArchitecture.Secrets.Exceptions;

public class InvalidEnvironmentVariableEncryptionException(string variable) : ConfigurationException($"Invalid Environment Variable '{variable}' Encryption!")
{
    public string Variable { get; } = variable;
}
