using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public IEnumerable<User> Get()
    {
        return _repo.GetAll();
    }

    [Authorize]
    // GET api/user/1
    [HttpGet("{id}")]
    public User Get(int id)
    {
        return _repo.Get(id);
    }
}
