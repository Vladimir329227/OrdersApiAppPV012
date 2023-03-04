using OrdersApiAppPV012.Model.Entity;

namespace OrdersApiAppPV012.Service.ClientService
{
    public interface IDaoOrder
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> AddOrder(Order order);
        Task<Order> UpdateOrder(Order client);
        Task<bool> DeleteOrder(int id);
    }
}
