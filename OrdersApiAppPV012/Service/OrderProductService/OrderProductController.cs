using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model.Entity;
using OrdersApiAppPV012.Model;

namespace OrdersApiAppPV012.Service.OrderProductService
{
    public class OrderProductController : IDaoOrderProduct                  // CRUD операции для OrderProduct
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
                throw new NotImplementedException();
            }

            var orderProduct = _context.OrderProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderProduct == null)
            {
                throw new NotImplementedException();
            }

            return Task.Run(() => orderProduct);
        }

        public Task<OrderProduct> Add(OrderProduct orderProduct)
        {
            if (orderProduct != null)
            {
                _context.Add(orderProduct);
                _context.SaveChangesAsync();
                return Task.Run(() => orderProduct);
            }
            return Task.Run(() => orderProduct);
        }

        public Task<OrderProduct> Update(int id, OrderProduct orderProduct)
        {
            if (id != orderProduct.Id)
            {
                throw new NotImplementedException();
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
                        throw new NotImplementedException();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Task.Run(() => orderProduct);
            }
            throw new NotImplementedException();
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