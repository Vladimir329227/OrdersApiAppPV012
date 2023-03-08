using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model;
using OrdersApiAppPV012.Model.BusinessLogic;
using OrdersApiAppPV012.Model.Entity;
using System.Collections.Generic;

namespace OrdersApiAppPV012.Service.BillService
{
    public class BillController : IDaoBill
    {
        private readonly ApplicationDbContext _context;

        public BillController(ApplicationDbContext context)
        {
            _context = context;
        }

        public Bill GetBill(int Id)
        {
            _context.Orders.Load();
            _context.Products.Load();
            List<OrderProduct> chek = _context.OrderProducts.ToList().FindAll(elem => (elem.OrderId == Id));
            int Prise = 0;
            foreach (var product in chek)
            {
                Prise += product.Product.Price * product.Count;
            }
            return  new Bill(chek, Prise);
        }
    }
}