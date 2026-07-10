using UserWebApi.Models;

namespace UserWebApi.Responses;

public class DetailsResponse
{
    public required User User { get; set; }
    public required IEnumerable<Book> Books { get; set; }
}
