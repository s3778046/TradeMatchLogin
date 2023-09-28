using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories;

namespace TradeMatchLogin.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserRepository _repo;

    public UserController(UserRepository repo)
    {
        _repo = repo;
    }

    // GET: api/user
    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        return await _repo.GetAllAsync();
    }

    [Authorize]
    // GET api/user/1
    [HttpGet("{id}")]
    public async Task<User> Get(Guid id)
    {
        return await _repo.FindAsync(id);
    }

    [Authorize]
    // PUT api/user
    [HttpPut]
    public async Task<User> Put([FromBody] User user)
    {
        await _repo.UpdateAsync(user.UserID, user);

        return user;
    }

    [Authorize]
    // DELETE api/user/1
    [HttpDelete("{id}")]
    public async Task<Guid> Delete(Guid id)
    {
       await _repo.DeleteAsync(id);

        return id;
    }
}
