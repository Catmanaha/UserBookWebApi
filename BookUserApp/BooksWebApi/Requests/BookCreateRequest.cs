namespace BooksWebApi.Requests;

public class BookCreateRequest
{
    public required string Name { get; set; }
    public required string Author { get; set; }
    public required string[] Tags { get; set; }
    public int UserId { get; set; }
}
