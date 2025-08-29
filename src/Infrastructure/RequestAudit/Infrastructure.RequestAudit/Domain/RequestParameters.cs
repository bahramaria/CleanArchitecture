namespace Infrastructure.RequestAudit.Domain;

public sealed class RequestParameters(int? orderId)
{
    public static readonly RequestParameters Empty = new RequestParameters(null);

    public int? OrderId { get; } = orderId;

    public RequestParameters Update(RequestParameters? response)
    {
        return new RequestParameters(OrderId ?? response?.OrderId);
    }

    public override string ToString()
    {
        return $"{OrderId}";
    }
}
