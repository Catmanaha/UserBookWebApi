using Microsoft.EntityFrameworkCore;
using UserWebApi.Data;
using UserWebApi.Models;
using UserWebApi.Requests;
using UserWebApi.Responses;
using UserWebApi.Services.Base;

namespace UserWebApi.Services;

public class UserMsSqlService(MyDbContext context, HttpClient client) : IUserService
{    
    public async Task Create(UserCreateRequest user)
    {
        await context.Users.AddAsync(new User
        {
            Name = user.Name
        });

        await context.SaveChangesAsync();
    }

    public async Task<DetailsResponse> GetDetails(int userId)
    {
        var request = await client.GetFromJsonAsync<IEnumerable<Book>>($"api/Books/GetAllForUser?id={userId}");
        var user = await context.Users.FirstOrDefaultAsync(o => o.Id == userId);

        return new DetailsResponse
        {
            User = user,
            Books = request
        };
    }
}
