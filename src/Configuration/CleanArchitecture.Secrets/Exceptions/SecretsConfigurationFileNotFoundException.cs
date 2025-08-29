using Framework.Exceptions;

namespace CleanArchitecture.Secrets.Exceptions;

public class SecretsConfigurationFileNotFoundException(string filePath) : ConfigurationException($"Secrets Configuration File '{filePath}' is not exists!")
{
    public string FilePath { get; } = filePath;
}
