namespace CleanArchitecture.Actors;

public class BrokerActor(int brokerId, string username, string displayName, bool? isClerk) : Actor(Role.Broker, username, displayName)
{
    public int BrokerId { get; } = brokerId;
    public bool? IsClerk { get; } = isClerk;

    public override string ToString()
    {
        return $"[Broker {BrokerId}] . [{Username}] . [{DisplayName}]";
    }
}
