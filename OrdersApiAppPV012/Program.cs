using OrdersApiAppPV012.Model;
using OrdersApiAppPV012.Model.Entity;
using OrdersApiAppPV012.Service;
using OrdersApiAppPV012.Service.BillService;
using OrdersApiAppPV012.Service.ClientService;
using OrdersApiAppPV012.Service.OrderInfoService;
using OrdersApiAppPV012.Service.OrderService;
using OrdersApiAppPV012.Service.OrderProductService;
using OrdersApiAppPV012.Service.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OrdersApiAppPV012.Model.JWT;

var builder = WebApplication.CreateBuilder(args);

// добавление зависимостей
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IDaoClient, ClientsController>();
builder.Services.AddTransient<IDaoOrder, OrderController>();
builder.Services.AddTransient<IDaoOrderProduct, OrderProductController>();
builder.Services.AddTransient<IDaoProduct, ProductController>();
builder.Services.AddTransient<IDaoOrderInfo, OrderInfoController>();
builder.Services.AddTransient<IDaoBill, BillController>();



builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });



var app = builder.Build();



app.UseAuthentication();
app.UseAuthorization();

app.Map("/login/{username}", (string username) =>
{
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
    // создаем JWT-токен
    var jwt = new JwtSecurityToken(
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));


    return new JwtSecurityTokenHandler().WriteToken(jwt);
});

app.Map("/data", [Authorize] () => new { message = "Hello World!" });



app.MapGet("/", () => "Hello World!");

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