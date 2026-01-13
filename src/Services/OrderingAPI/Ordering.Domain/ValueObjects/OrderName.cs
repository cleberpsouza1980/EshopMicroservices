namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    public string Value { get; } = default!;

    private OrderName(string value)
    {
        Value = value;
    }

    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        if (value.Length > 100)
        {
            throw new ArgumentException("OrderName cannot exceed 100 characters.");
        }
        if (value==string.Empty)
        {
            throw new ArgumentException("OrderName cannot null.");
        }
        return new OrderName(value);
    }
}
