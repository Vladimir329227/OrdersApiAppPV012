namespace OrdersApiAppPV012.Model.Entity
{
    public class Product
    {
        public int Id { get; set; }             // первичный ключ
        public string Name { get; set; }        // название продукта
        public int Code { get; set; }           // код продукта
        public int Price { get; set; }

    }
}
