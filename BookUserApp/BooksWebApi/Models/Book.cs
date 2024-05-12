using MongoDB.Bson.Serialization.Attributes;

namespace BooksWebApi.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string[] Tags { get; set; }
    public int UserId { get; set; }

}
