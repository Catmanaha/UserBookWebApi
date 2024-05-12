using Microsoft.EntityFrameworkCore;
using UserWebApi.Data;
using UserWebApi.Services;
using UserWebApi.Services.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddHttpClient<IUserService, UserMsSqlService>(o =>
{
    var baseAddress = builder.Configuration.GetSection("BooksApiBaseAddress").Value;

    if (baseAddress != null)
    {
        o.BaseAddress = new Uri(baseAddress);
    }
});

builder.Services.AddDbContext<MyDbContext>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    o.UseSqlServer(connectionString);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapDefaultControllerRoute();

app.UseHttpsRedirection();

app.Run();
