using UrlShortner.data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

//Redis

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:Connection"];
});

builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();