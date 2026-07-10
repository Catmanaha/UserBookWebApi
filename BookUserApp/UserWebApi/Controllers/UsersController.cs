using Microsoft.AspNetCore.Mvc;
using UserWebApi.Requests;
using UserWebApi.Responses;
using UserWebApi.Services.Base;

namespace UserWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(UserCreateRequest user)
    {
        await userService.Create(user);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<DetailsResponse>> Details(int id)
    {
        var details = await userService.GetDetails(id);
        return details is null ? NotFound() : Ok(details);
    }
}
