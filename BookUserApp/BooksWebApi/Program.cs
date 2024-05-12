using BooksWebApi.Options;
using BooksWebApi.Services;
using BooksWebApi.Services.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.Configure<MongoDbOption>(builder.Configuration.GetSection("MongoDbOption"));

builder.Services.AddScoped<IBookService, BookMongoService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapDefaultControllerRoute();

app.Run();
