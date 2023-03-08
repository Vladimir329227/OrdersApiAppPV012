using OrdersApiAppPV012.Model;
using OrdersApiAppPV012.Model.Entity;
using OrdersApiAppPV012.Service;
using OrdersApiAppPV012.Service.BillService;
using OrdersApiAppPV012.Service.ClientService;
using OrdersApiAppPV012.Service.OrderInfoService;
using OrdersApiAppPV012.Service.OrderService;
using OrdersApiAppPV012.Service.OrderProductService;
using OrdersApiAppPV012.Service.ProductService;

var builder = WebApplication.CreateBuilder(args);

// добавление зависимостей
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IDaoClient, ClientsController>();
builder.Services.AddTransient<IDaoOrder, OrderController>();
builder.Services.AddTransient<IDaoOrderProduct, OrderProductController>();
builder.Services.AddTransient<IDaoProduct, ProductController>();
builder.Services.AddTransient<IDaoOrderInfo, OrderInfoController>();
builder.Services.AddTransient<IDaoBill, BillController>();



var app = builder.Build();


app.MapGet("/", () => "Hello World!");

// тестирование операций с таблицей клиента

//Client
app.MapGet("/client/all", async (HttpContext context, IDaoClient dao) =>
{
    return await dao.GetAll();
});

app.MapGet("/client/", async (HttpContext context,int Id, IDaoClient dao) =>
{
    return await dao.GetById(Id);
});

app.MapPost("/client/add", async (HttpContext context, Client client, IDaoClient dao) =>
{
    return await dao.Add(client);
});

app.MapPost("/client/update", async (HttpContext context, int Id, Client client, IDaoClient dao) =>
{
    return await dao.Update(Id, client);
});

app.MapGet("/client/delete", async (HttpContext context, int Id, IDaoClient dao) =>
{
    return await dao.Delete(Id);
});

//Order
app.MapGet("/order/all", async (HttpContext context, IDaoOrder dao) =>
{
    return await dao.GetAll();
});

app.MapGet("/order/", async (HttpContext context, int Id, IDaoOrder dao) =>
{
    return await dao.GetById(Id);
});

app.MapPost("/order/add", async (HttpContext context, Order order, IDaoOrder dao) =>
{
    return await dao.Add(order);
});

app.MapPost("/order/update", async (HttpContext context, int Id, Order order, IDaoOrder dao) =>
{
    return await dao.Update(Id, order);
});

app.MapGet("/order/delete", async (HttpContext context, int Id, IDaoOrder dao) =>
{
    return await dao.Delete(Id);
});

//OrderProduct
app.MapGet("/orderProduct/all", async (HttpContext context, IDaoOrderProduct dao) =>
{
    return await dao.GetAll();
});

app.MapGet("/orderProduct/", async (HttpContext context, int Id, IDaoOrderProduct dao) =>
{
    return await dao.GetById(Id);
});

app.MapPost("/orderProduct/add", async (HttpContext context, OrderProduct orderProduct, IDaoOrderProduct dao) =>
{
    return await dao.Add(orderProduct);
});

app.MapPost("/orderProduct/update", async (HttpContext context, int Id, OrderProduct orderProduct, IDaoOrderProduct dao) =>
{
    return await dao.Update(Id, orderProduct);
});

app.MapGet("/orderProduct/delete", async (HttpContext context, int Id, IDaoOrderProduct dao) =>
{
    return await dao.Delete(Id);
});

//Product
app.MapGet("/product/all", async (HttpContext context, IDaoProduct dao) =>
{
    return await dao.GetAll();
});

app.MapGet("/product/", async (HttpContext context, int Id, IDaoProduct dao) =>
{
    return await dao.GetById(Id);
});

app.MapPost("/product/add", async (HttpContext context, Product product, IDaoProduct dao) =>
{
    return await dao.Add(product);
});

app.MapPost("/product/update", async (HttpContext context, int Id, Product product, IDaoProduct dao) =>
{
    return await dao.Update(Id, product);
});

app.MapGet("/product/delete", async (HttpContext context, int Id, IDaoProduct dao) =>
{
    return await dao.Delete(Id);
});

// бизнес логика

app.MapGet("/order/bill",  (HttpContext context, int Id, IDaoBill dao) =>
{
    return dao.GetBill(Id).ToString();
});

app.MapGet("/order/info", async (HttpContext context, int Id, IDaoOrderInfo dao) =>
{
    return await dao.GetInfo(Id);
});

app.Run();