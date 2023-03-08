using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model.Entity;
using OrdersApiAppPV012.Model;

namespace OrdersApiAppPV012.Service.ProductService
{
    public class ProductController : IDaoProduct                                // CRUD операции для Product
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
                throw new NotImplementedException();
            }

            var product = _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                throw new NotImplementedException();
            }

            return Task.Run(() => product);
        }

        public Task<Product> Add(Product product)
        {
            if (product != null)
            {
                _context.Add(product);
                _context.SaveChangesAsync();
                return Task.Run(() => product);
            }
            return Task.Run(() => product);
        }

        public Task<Product> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                throw new NotImplementedException();
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
                        throw new NotImplementedException();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Task.Run(() => product);
            }
            throw new NotImplementedException();
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