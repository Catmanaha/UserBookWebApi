using BooksWebApi.Models;
using BooksWebApi.Options;
using BooksWebApi.Requests;
using BooksWebApi.Services.Base;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BooksWebApi.Services;

public class BookMongoService(IOptionsSnapshot<MongoDbOption> optionsSnapshot) : IBookService
{
    private readonly MongoClient client = new MongoClient(optionsSnapshot.Value.MongoDbConnectionString);
    private readonly string databaseName = optionsSnapshot.Value.DatabaseName;
    private readonly string collectionName = optionsSnapshot.Value.CollectionName;

    public async Task Create(BookCreateRequest book)
    {
        var db = client.GetDatabase(databaseName);
        db.CreateCollection(collectionName);

        await db.GetCollection<Book>(collectionName).InsertOneAsync(new Book
        {
            Name = book.Name,
            Author = book.Author,
            Tags = book.Tags,
            UserId = book.UserId
        });
    }

    public async Task<IEnumerable<Book>> GetAllForUser(int userId)
    {
        var db = client.GetDatabase(databaseName);
        db.CreateCollection(collectionName);
        var books = db.GetCollection<Book>(collectionName);
        return await books.Find(Builders<Book>.Filter.Eq("UserId", userId)).ToListAsync();
    }
}
