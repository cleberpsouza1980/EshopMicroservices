using ShoppingWeb.Models.Ordering;
using System.Globalization;

namespace ShoppingWeb.Services
{
    public interface IOrderSevice
    {
        Task<GetOrdersResponse> GetOrder();
        Task<GetOrdersByNameResponse> GetOrderByName(string name);
        Task<GetOrdersByCustomerResponse> GetOrderByCustomer(Guid  customerId);
    }
}
