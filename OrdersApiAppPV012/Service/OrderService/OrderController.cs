using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model.Entity;
using OrdersApiAppPV012.Model;
using static NuGet.Packaging.PackagingConstants;

namespace OrdersApiAppPV012.Service.OrderService
{
    public class OrderController : IDaoOrder                            // CRUD операции для Order
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool OrdersExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public Task<List<Order>> GetAll()
        {
            return Task.Run(() => _context.Orders.ToListAsync());
        }

        public Task<Order> GetById(int id)
        {
            if (id == null || _context.Orders == null)
            {
                throw new NotImplementedException();
            }

            var order = _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                throw new NotImplementedException();
            }

            return Task.Run(() => order);
        }

        public Task<Order> Add(Order order)
        {
            if (order != null)
            {
                _context.Add(order);
                _context.SaveChangesAsync();
                return Task.Run(() => order);
            }
            return Task.Run(() => order);
        }

        public Task<Order> Update(int id, Order order)
        {
            if (id != order.Id)
            {
                throw new NotImplementedException();
            }

            if (order != null)
            {
                try
                {
                    _context.Update(order);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(order.Id))
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Task.Run(() => order);
            }
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            Order? order = await _context.Orders.FirstOrDefaultAsync((order) => order.Id == id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            return order != null;
        }
    }
}