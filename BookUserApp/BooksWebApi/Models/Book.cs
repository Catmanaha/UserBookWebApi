using MongoDB.Bson.Serialization.Attributes;

namespace BooksWebApi.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    public required string[] Tags { get; set; }
    public int UserId { get; set; }

}
