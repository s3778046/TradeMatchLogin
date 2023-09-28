using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleHashing.Net;
using TradeMatchLogin.Dtos;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories;


namespace TradeMatchLogin.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginRepository _repo;
    private static readonly ISimpleHash simpleHash = new SimpleHash();

    public LoginController(LoginRepository repo)
    {
        _repo = repo;
    }

    // PUT api/login/{id}
    [HttpPut]
    public async Task<Login> Put([FromBody] Login login)
    {
        // Hash the password input
        login.PasswordHash = simpleHash.Compute(login.PasswordHash);

        await _repo.UpdateAsync(login.LoginID, login);

        return login;
    }

    // DELETE api/login/{id}
    [HttpDelete("{id}")]
    public async Task<Guid> Delete(Guid id)
    {
       await _repo.DeleteAsync(id);

        return id;
    }
}
