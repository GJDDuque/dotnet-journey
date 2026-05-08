using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;
using OrdersApi.Services;

var builder = WebApplication.CreateBuilder(args);

// REGISTRATION PHASE — add services to the DI container
builder.Services.AddControllers();

// Register DB context
builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// PIPELINE PHASE — configure the middleware pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();