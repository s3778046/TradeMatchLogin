using Microsoft.EntityFrameworkCore;
using TradeMatchLogin.DataContext;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories.Interface;

namespace TradeMatchLogin.Repositories;

public class LoginRepository : AGenericRepository<Login, Guid>, ILoginRepository
{

    public LoginRepository(TradeMatchContext context) : base(context) { }


    public override async Task<IEnumerable<Login>> GetAllAsync()
    {
        return await base.GetAllAsync();
    }

    public override async Task<Login> AddAsync(Login entity)
    {
        return await base.AddAsync(entity);
    }

    public override async Task<Login> DeleteAsync(Guid key)
    {
        return await base.DeleteAsync(key);
    }

    public override async Task<Login> FindAsync(Guid key)
    {
        return await base.FindAsync(key);
    }

    public override async Task<Login> UpdateAsync(Guid key, Login entity)
    {
        return await base.UpdateAsync(key, entity);
    }















    public async Task<Login> GetByUserNameAsync(string username)
    {
        var login = await _context.Login.FirstOrDefaultAsync(x => x.UserName == username);

        return login;
    }
}
