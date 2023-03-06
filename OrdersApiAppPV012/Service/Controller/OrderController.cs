using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model.Entity;
using OrdersApiAppPV012.Model;
using static NuGet.Packaging.PackagingConstants;
using OrdersApiAppPV012.Service.IDao;

namespace OrdersApiAppPV012.Service.Controllers
{
    public class OrderController : IDaoOrder
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
                return null;
            }

            var order = _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return null;
            }

            return Task.Run(() => order);
        }

        public Task<Order> Add(Order order)
        {
            if (order == null)
            {
                _context.Add(order);
                _context.SaveChangesAsync();
                return null;
            }
            return Task.Run(() => order);
        }

        public Task<Order> Update(int id, Order order)
        {
            if (id != order.Id)
            {
                return null;
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
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
                return Task.Run(() => order);
            }
            return null;
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