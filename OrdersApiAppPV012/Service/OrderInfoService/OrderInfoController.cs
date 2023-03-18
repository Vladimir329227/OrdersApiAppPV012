using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model;
using OrdersApiAppPV012.Model.BusinessLogic;
using OrdersApiAppPV012.Model.Entity;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace OrdersApiAppPV012.Service.OrderInfoService
{
    public class OrderInfoController : IDaoOrderInfo
    {
        private readonly ApplicationDbContext _context;

        public OrderInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<OrderProduct>> GetInfo(int Id)
        {
            _context.Orders.Load();
            _context.Products.Load();
            return Task.Run(() => _context.OrderProducts.ToList().FindAll(elem => (elem.OrderId == Id)));
        }
    }
}
