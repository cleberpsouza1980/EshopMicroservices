
namespace BasketAPI.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string message) : base("Basket", message)
    {
    }
}
