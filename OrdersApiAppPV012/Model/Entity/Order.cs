namespace OrdersApiAppPV012.Model.Entity
{
    public class Order
    {
        public int Id { get; set; }                     // первичный ключ
        public string Description { get; set; }         // описание заказа
        public int ClientId { get; set; }               // внешний ключ на клиента
        public  Client Client { get; set; }             // объект клиента, на которого ссылается заказ 

        public Order()
        {
            Description = "";
        }

        public override string ToString()
        {
            return $"{Id} - {Description} - {ClientId}";
        }
    }
}
