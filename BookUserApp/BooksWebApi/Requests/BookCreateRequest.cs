namespace BooksWebApi.Requests;

public class BookCreateRequest
{
    public string Name { get; set; }
    public string Author { get; set; }
    public string[] Tags { get; set; }
    public int UserId { get; set; }
}
