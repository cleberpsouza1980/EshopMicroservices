namespace Ordering.Domain.ValueObjects;

public record CustumerId
{
    public Guid Value { get; }    

    private CustumerId(Guid value)
    {
        Value = value;
    }

    public static CustumerId  Of(Guid value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value.ToString());

        if (value == Guid.Empty)
        {
            throw new ArgumentException("CustumerId cannot be empty.");
        }
        return new CustumerId(value);
    }
}
