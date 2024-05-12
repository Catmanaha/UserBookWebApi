using BooksWebApi.Models;
using BooksWebApi.Requests;

namespace BooksWebApi.Services.Base;

public interface IBookService
{
    public Task Create(BookCreateRequest book);
    public Task<IEnumerable<Book>> GetAllForUser(int userId);
}
