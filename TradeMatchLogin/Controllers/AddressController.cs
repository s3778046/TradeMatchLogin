using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories;

namespace TradeMatchLogin.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly AddressRepository _repo;

    public AddressController(AddressRepository repo)
    {
        _repo = repo;
    }

    // PUT api/address/{id}
    [HttpPut]
    public async Task<Address> Put([FromBody] Address address)
    {
        await _repo.UpdateAsync(address.AddressID, address);

        return address;
    }

    // DELETE api/address/{id}
    [HttpDelete("{id}")]
    public async Task<Guid> Delete(Guid id)
    {
       await _repo.DeleteAsync(id);

        return id;
    }
}
