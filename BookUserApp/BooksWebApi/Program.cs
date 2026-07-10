using BooksWebApi.Options;
using BooksWebApi.Services;
using BooksWebApi.Services.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services
    .AddOptions<MongoDbOption>()
    .Bind(builder.Configuration.GetSection("MongoDbOption"))
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.MongoDbConnectionString),
        "MongoDbOption:MongoDbConnectionString is required"
    )
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.DatabaseName),
        "MongoDbOption:DatabaseName is required"
    )
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.CollectionName),
        "MongoDbOption:CollectionName is required"
    )
    .ValidateOnStart();

builder.Services.AddScoped<IBookService, BookMongoService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapDefaultControllerRoute();

app.Run();
