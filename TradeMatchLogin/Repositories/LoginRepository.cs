using Microsoft.EntityFrameworkCore;
using TradeMatchLogin.DataContext;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories.Interface;

namespace TradeMatchLogin.Repositories;

public class LoginRepository : IDataRepository<Login, int>
{
    private readonly TradeMatchContext _context;

    public LoginRepository(TradeMatchContext context)
    {
        _context = context;
    }

    public Login Get(int id)
    {
        return _context.Login.Find(id);
    }

    public async Task<Login> GetByUserNameAsync(string username)
    {
        var login = await _context.Login.FirstOrDefaultAsync(x => x.UserName == username);

        return login;
    }

    public IEnumerable<Login> GetAll()
    {
        return _context.Login.ToList();
    }

    public int Add(Login login)
    {
        _context.Login.Add(login);
        _context.SaveChanges();

        return login.LoginID;
    }

    public int Delete(int id)
    {
        _context.Login.Remove(_context.Login.Find(id));
        _context.SaveChanges();

        return id;
    }

    public int Update(int id, Login login)
    {
        _context.Update(login);
        _context.SaveChanges();

        return id;
    }

    IEnumerable<Login> IDataRepository<Login, int>.GetAll()
    {
        throw new NotImplementedException();
    }

    Login IDataRepository<Login, int>.Get(int id)
    {
        throw new NotImplementedException();
    }

}
