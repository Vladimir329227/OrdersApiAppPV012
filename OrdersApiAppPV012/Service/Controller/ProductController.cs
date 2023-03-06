using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model.Entity;
using OrdersApiAppPV012.Model;
using OrdersApiAppPV012.Service.IDao;

namespace OrdersApiAppPV012.Service.Controllers
{
    public class ProductController : IDaoProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public Task<List<Product>> GetAll()
        {
            return Task.Run(() => _context.Products.ToListAsync());
        }

        public Task<Product> GetById(int id)
        {
            if (id == null || _context.Products == null)
            {
                return null;
            }

            var product = _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return null;
            }

            return Task.Run(() => product);
        }

        public Task<Product> Add(Product product)
        {
            if (product == null)
            {
                _context.Add(product);
                _context.SaveChangesAsync();
                return null;
            }
            return Task.Run(() => product);
        }

        public Task<Product> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return null;
            }

            if (product != null)
            {
                try
                {
                    _context.Update(product);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
                return Task.Run(() => product);
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync((product) => product.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            return product != null;
        }
    }
}