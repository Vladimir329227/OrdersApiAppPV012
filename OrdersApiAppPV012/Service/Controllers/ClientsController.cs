using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model;
using OrdersApiAppPV012.Model.Entity;
using OrdersApiAppPV012.Service.IDao;

namespace OrdersApiAppPV012.Service.Controllers
{
    public class ClientsController : IDaoClient
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool ClientExists(int id)
        {
          return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public Task<List<Client>> GetAll()
        {
            return Task.Run(() => _context.Clients.ToListAsync());
        }

        public Task<Client> GetById(int id)
        {
            if (id == null || _context.Clients == null)
            {
                return null;
            }

            var client = _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return null;
            }

            return Task.Run(() => client);
        }

        public Task<Client> Add(Client client)
        {
            if (client == null)
            {
                _context.Add(client);
                _context.SaveChangesAsync();
                return null;
            }
            return Task.Run(() => client);
        }

        public Task<Client> Update(int id,Client client)
        {
            if (id != client.Id)
            {
                return null;
            }

            if (client != null)
            {
                try
                {
                    _context.Update(client);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
                return Task.Run(() => client);
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            Client? client = await _context.Clients.FirstOrDefaultAsync((client) => client.Id == id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
            return client != null;
        }
    }
}
