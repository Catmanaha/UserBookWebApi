namespace BooksWebApi.Options;

public class MongoDbOption
{
    public required string MongoDbConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string CollectionName { get; set; }
}
