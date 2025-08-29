namespace Framework.Domain.IntegrationEvents;

public abstract class IntegrationEvent(DateTime eventTime) : IIntegrationEvent
{
    public long EventId { get; }
    public Guid EventGuid { get; } = Guid.NewGuid();
    public DateTime EventTime { get; } = eventTime;
    public IntegrationEventPublishStatus PublishStatus { get; private set; } = IntegrationEventPublishStatus.InProcess;
    public int PublishTryCount { get; private set; } = 0;
    public Guid? CorrelationId { get; set; }

    public void Update(IntegrationEventPublishStatus publishStatus, int publishTryCount)
    {
        PublishStatus = publishStatus;
        PublishTryCount = publishTryCount;
    }
}
