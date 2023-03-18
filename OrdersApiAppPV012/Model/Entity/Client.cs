﻿namespace OrdersApiAppPV012.Model.Entity
{
    // сущность клиента - класс-хранилка данных о клиенте (DTO - data transfer object, data-class)
    public class Client
    {
        public int Id { get; set; }                         // первичный ключ
        public string Name { get; set; }                    // навигационные сво-ва
        public ICollection<Order> Orders { get; set; }      // список заказа клиента

        public Client()
        {
            Name = "";
        }

        public Client(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
