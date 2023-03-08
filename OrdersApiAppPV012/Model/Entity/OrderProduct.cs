namespace OrdersApiAppPV012.Model.Entity
{
    public class OrderProduct
    {
        public int Id { get; set; }                 // первичный ключ
        public int ProductId { get; set; }          // внешний ключ на продукт
        public Product Product { get; set; }        // навигационные сво-ва для продукта
        public int OrderId { get; set; }            //внешний ключ на заказ
        public Order Order { get; set; }            //навигационные сво-ва для заказа
        public int Count { get; set; }              // количество продукта
    }
}
