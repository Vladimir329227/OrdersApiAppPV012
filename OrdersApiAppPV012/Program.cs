using OrdersApiAppPV012.Model;
using OrdersApiAppPV012.Model.Entity;
using OrdersApiAppPV012.Service.ClientService;

var builder = WebApplication.CreateBuilder(args);

// добавление зависимостей
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IDaoClient, DbdDaoClient>();
builder.Services.AddTransient<IDaoOrder, DBDaoOrder>();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

// тестирование операций с таблицей клиента

app.MapGet("/client/all", async (HttpContext context, IDaoClient dao) =>
{
    return await dao.GetAllClients();
});

app.MapPost("/client/add", async (HttpContext context, Client client, IDaoClient dao) =>
{
    return await dao.AddClient(client);
});

app.MapGet("/order/all", async (HttpContext context, IDaoOrder dao) =>
{
    return await dao.GetAllOrders();
});

app.MapPost("/order/add", async (HttpContext context, Order order, IDaoOrder dao) =>
{
    return await dao.AddOrder(order);
});

app.Run();
