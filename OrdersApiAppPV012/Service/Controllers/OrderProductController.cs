using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model.Entity;
using OrdersApiAppPV012.Model;
using OrdersApiAppPV012.Service.IDao;

namespace OrdersApiAppPV012.Service.Controllers
{
    public class OrderProductController : IDaoOrderProduct
    {
        private readonly ApplicationDbContext _context;

        public OrderProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool OrderProductExists(int id)
        {
            return (_context.OrderProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public Task<List<OrderProduct>> GetAll()
        {
            return Task.Run(() => _context.OrderProducts.ToListAsync());
        }

        public Task<OrderProduct> GetById(int id)
        {
            if (id == null || _context.OrderProducts == null)
            {
                return null;
            }

            var orderProduct = _context.OrderProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderProduct == null)
            {
                return null;
            }

            return Task.Run(() => orderProduct);
        }

        public Task<OrderProduct> Add(OrderProduct orderProduct)
        {
            if (orderProduct == null)
            {
                _context.Add(orderProduct);
                _context.SaveChangesAsync();
                return null;
            }
            return Task.Run(() => orderProduct);
        }

        public Task<OrderProduct> Update(int id, OrderProduct orderProduct)
        {
            if (id != orderProduct.Id)
            {
                return null;
            }

            if (orderProduct != null)
            {
                try
                {
                    _context.Update(orderProduct);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderProductExists(orderProduct.Id))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
                return Task.Run(() => orderProduct);
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            OrderProduct? orderProduct = await _context.OrderProducts.FirstOrDefaultAsync((orderProduct) => orderProduct.Id == id);
            if (orderProduct != null)
            {
                _context.OrderProducts.Remove(orderProduct);
            }
            return orderProduct != null;
        }
    }
}