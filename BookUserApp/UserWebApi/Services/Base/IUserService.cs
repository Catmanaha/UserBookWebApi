using UserWebApi.Requests;
using UserWebApi.Responses;

namespace UserWebApi.Services.Base;

public interface IUserService
{
    public Task Create(UserCreateRequest user);
    public Task<DetailsResponse> GetDetails(int userId);
}
