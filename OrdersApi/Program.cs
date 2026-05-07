using OrdersApi.Services;

var builder = WebApplication.CreateBuilder(args);

// REGISTRATION PHASE — add services to the DI container
builder.Services.AddControllers();

builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// PIPELINE PHASE — configure the middleware pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();