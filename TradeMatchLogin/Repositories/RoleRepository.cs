using TradeMatchLogin.DataContext;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories.Interface;

namespace TradeMatchLogin.Repositories;

public class RoleRepository : IDataRepository<Role, int>
{
    private readonly TradeMatchContext _context;

    public RoleRepository(TradeMatchContext context)
    {
        _context = context;
    }

    public Role Get(int id)
    {
        return _context.Role.Find(id);
    }

    public IEnumerable<Role> GetAll()
    {
        return _context.Role.ToList();
    }

    public int Add(Role role)
    {
        _context.Role.Add(role);
        _context.SaveChanges();

        return role.RoleID;
    }

    public int Delete(int id)
    {
        _context.Role.Remove(_context.Role.Find(id));
        _context.SaveChanges();

        return id;
    }

    public int Update(int id, Role role)
    {
        _context.Update(role);
        _context.SaveChanges();

        return id;
    }

    IEnumerable<Role> IDataRepository<Role, int>.GetAll()
    {
        throw new NotImplementedException();
    }

    Role IDataRepository<Role, int>.Get(int id)
    {
        throw new NotImplementedException();
    }
}
