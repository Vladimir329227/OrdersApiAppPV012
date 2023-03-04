using Microsoft.EntityFrameworkCore;
using OrdersApiAppPV012.Model.Entity;

namespace OrdersApiAppPV012.Model
{
    //Класс адаптер для выполнения операций с БД
    public class ApplicationDbContext : DbContext
    {
        //Таблицы
        public DbSet<Client> Clients { get;set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        // конфигурация контекста
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // получаем файл конфигурации
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            // устанавливаем для контекста строку подключения
            // инициализируем саму строку подключения
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
