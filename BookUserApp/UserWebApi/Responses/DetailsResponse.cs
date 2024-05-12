using UserWebApi.Models;

namespace UserWebApi.Responses;

public class DetailsResponse
{
    public User User { get; set; }
    public IEnumerable<Book> Books { get; set; }
}
