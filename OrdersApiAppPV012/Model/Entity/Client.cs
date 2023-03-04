namespace OrdersApiAppPV012.Model.Entity
{
    // сущность клиента - класс-хранилка данных о клиенте (DTO - data transfer object, data-class)
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // навигационные сво-ва
        public ICollection<Order> Orders { get; set; }

        public Client()
        {
            Orders = new List<Order>();
            Name = "";
        }

        public Client(int id, string name)
        {
            Id = id;
            Name = name;
            Orders = new List<Order>();
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
