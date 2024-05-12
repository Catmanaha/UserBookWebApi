namespace BooksWebApi.Options;

public class MongoDbOption
{
    public string MongoDbConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}
