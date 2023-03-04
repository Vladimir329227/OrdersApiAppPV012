using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrdersApiAppPV012.Model;
using OrdersApiAppPV012.Model.Entity;

namespace OrdersApiAppPV012.Service.ClientService
{
    public class DbdDaoClient : IDaoClient
    {
        private readonly ApplicationDbContext _context;

        public DbdDaoClient(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Client>> GetAllClients()
        {
            _context.Clients.Load();
            return Task.Run(() => _context.Clients.ToListAsync());
        }

        public Task<Client> GetClientById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> AddClient(Client client)
        {
            _context.Add(client);
            _context.SaveChanges();
            return Task.Run(() => client);  
        }

        public Task<Client> UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClient(int id)
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
