using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model;
using OrdersApiAppPV012.Model.Entity;

namespace OrdersApiAppPV012.Service.ClientService
{
    public class DBDaoOrder : IDaoOrder
    {
        private readonly ApplicationDbContext _context;

        public DBDaoOrder(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Order>> GetAllOrders()
        {
            return Task.Run(() => _context.Orders.ToListAsync());
        }

        public Task<Order> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> AddOrder(Order order)
        {
            order.Id = _context.Orders.Count();
            _context.Add(order);
            _context.SaveChanges();
            return Task.Run(() => order);
        }

        public Task<Order> UpdateOrder(Order client)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
