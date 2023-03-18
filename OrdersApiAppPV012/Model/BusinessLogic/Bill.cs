using OrdersApiAppPV012.Model.Entity;
using static Azure.Core.HttpHeader;

namespace OrdersApiAppPV012.Model.BusinessLogic
{
    public class Bill
    {
        public List<OrderProduct> OrderProducts;
        public int Price;

        public Bill(List<OrderProduct> OrderProducts, int Price)
        {
            this.OrderProducts = OrderProducts;
            this.Price = Price;
        }

        public override string ToString()
        {
            string textChek = "";
            foreach (var product in OrderProducts)
            {
                textChek += product.Product.Name + "----------" + product.Product.Price + "*" + product.Count + "\n";
            }

            return $"{textChek} - {Price}";
        }
    }
}