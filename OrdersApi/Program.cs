var builder = WebApplication.CreateBuilder(args);

// REGISTRATION PHASE — add services to the DI container
builder.Services.AddControllers();

var app = builder.Build();

// PIPELINE PHASE — configure the middleware pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();