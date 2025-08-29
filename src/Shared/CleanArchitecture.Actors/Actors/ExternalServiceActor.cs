namespace CleanArchitecture.Actors;

public class ExternalServiceActor(string service, string serviceName) : Actor(Role.ExternalService, service, serviceName)
{
    public override string ToString()
    {
        return $"[External Service] . [{DisplayName}]";
    }
}
