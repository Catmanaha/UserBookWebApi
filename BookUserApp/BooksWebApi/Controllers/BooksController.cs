using BooksWebApi.Models;
using BooksWebApi.Requests;
using BooksWebApi.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Book>> GetAllForUser(int id)
    {
        return await bookService.GetAllForUser(id);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookCreateRequest book)
    {
        await bookService.Create(book);

        return Ok();
    }
}
